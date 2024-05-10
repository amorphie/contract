using amorphie.contract.application.Customer.Dto;
using amorphie.contract.application.Customer.Request;
using amorphie.contract.application.Extensions;
using amorphie.contract.infrastructure.Contexts;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Elastic.Apm.Api;
using amorphie.contract.core.Services;
using Microsoft.Extensions.Configuration;
using amorphie.contract.core.Response;
using amorphie.contract.core;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Extensions;
using amorphie.contract.core.Entity.Contract;
using System.Collections.Generic;
using amorphie.contract.core.Entity.Document;

namespace amorphie.contract.application.Customer
{
    public interface ICustomerAppService
    {
        Task<GenericResult<Guid>> GetIdByReference(string userReference);
        Task<GenericResult<Guid>> AddAsync(CustomerInputDto inputDto);
        Task<GenericResult<List<CustomerContractDto>>> GetDocumentsByContracts(GetCustomerDocumentsByContractInputDto inputDto, CancellationToken token);
        Task<GenericResult<List<DocumentCustomerDto>>> GetAllDocuments(GetCustomerDocumentsByContractInputDto inputDto, CancellationToken token);
        Task<GenericResult<bool>> DeleteAllDocuments(string reference, CancellationToken cts);
    }

    public partial class CustomerAppService : ICustomerAppService
    {
        private readonly ProjectDbContext _dbContext;
        private readonly IMinioService _minioService;

        private readonly string _baseUrl;
        private readonly string _downloadEndpoint;


        public CustomerAppService(ProjectDbContext dbContext, IMinioService minioService, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _minioService = minioService;

            _baseUrl = StaticValuesExtensions.Apisix.BaseUrl;
            _downloadEndpoint = StaticValuesExtensions.Apisix.DownloadEndpoint;
        }

        public async Task<GenericResult<Guid>> GetIdByReference(string userReference)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(userReference));

            var customerId = await _dbContext.Customer
                .Where(x => x.Reference == userReference)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            if (customerId == Guid.Empty)
            {
                return GenericResult<Guid>.Fail($"Customer not found. Reference: {userReference}");
            }

            return GenericResult<Guid>.Success(customerId);
        }

        public async Task<GenericResult<Guid>> AddAsync(CustomerInputDto inputDto)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(inputDto.Reference));

            var customer = new core.Entity.Customer
            {
                Reference = inputDto.Reference,
                Owner = inputDto.Owner,
                CustomerNo = inputDto.CustomerNo
            };

            _dbContext.Customer.Add(customer);

            await _dbContext.SaveChangesAsync();

            return GenericResult<Guid>.Success(customer.Id);
        }

        public async Task<GenericResult<List<CustomerContractDto>>> GetDocumentsByContracts(GetCustomerDocumentsByContractInputDto inputDto, CancellationToken token)
        {
            bool othersOnly = false;
            if (inputDto.Code == "//OtherDocuments")
            {
                inputDto.Code = "";
                othersOnly = true;
            }

            var userReference = inputDto.GetUserReference();

            var documentQuery = _dbContext.Document.Where(x => x.Customer.Reference == userReference).AsQueryable();
            var allContractQuery = _dbContext!.ContractDefinition.AsQueryable();
            allContractQuery = ContractHelperExtensions.LikeWhere(allContractQuery, inputDto.Code);


            if (inputDto.StartDate.HasValue)
            {
                documentQuery = documentQuery.Where(x => x.CreatedAt > inputDto.StartDate.Value);
            }

            if (inputDto.EndDate.HasValue)
            {
                documentQuery = documentQuery.Where(x => x.CreatedAt < inputDto.EndDate.Value);
            }

            var documents = await documentQuery.Select(x => new DocumentForMinioObject
            {
                Id = x.Id,
                DocumentDefinitionId = x.DocumentDefinitionId,
                Status = x.Status,
                DocumentContentId = x.DocumentContentId.ToString()
            })
            .AsSplitQuery()
            .ToListAsync(token);

            allContractQuery = allContractQuery
                .Where(x => x.ContractDocumentDetails.Any(z => documentQuery.Select(d => d.DocumentDefinitionId).Contains(z.DocumentDefinitionId))
                || x.ContractDocumentGroupDetails.Any(z => z.DocumentGroup.DocumentGroupDetails.Any(y => documentQuery.Select(d => d.DocumentDefinitionId).Contains(y.DocumentDefinitionId))));

            var history = _dbContext.ContractDefinitionHistory
                .Where(x => allContractQuery.Select(ab => ab.Id).Contains(x.ContractDefinitionId))
                .ToList();

            var historyQuery = history
                .Where(x => x.ContractDefinitionHistoryModel.ContractDocumentDetails.Any(z => documentQuery.Select(d => d.DocumentDefinitionId).Contains(z.DocumentDefinitionId))
                || x.ContractDefinitionHistoryModel.ContractDocumentGroupDetails.Any(z => z.DocumentGroup.DocumentGroupDetails.Any(y => documentQuery.Select(d => d.DocumentDefinitionId).Contains(y.DocumentDefinitionId))))
                .Select(x => x.ContractDefinitionHistoryModel);

            var contractHistoryQuery = historyQuery
                .Where(x => x.BankEntity == inputDto.GetBankEntityCode())
                .ToList();

            var contractHistory = ObjectMapperApp.Mapper.Map<List<ContractDefinition>>(contractHistoryQuery);
            var contractHistoryModels = ObjectMapperApp.Mapper.Map<List<CustomerContractDto>>(contractHistory);

            var contractQuery = allContractQuery.Where(x => x.BankEntity == inputDto.GetBankEntityCode());

            var contractModels = await contractQuery
                .AsNoTracking()
                .AsSplitQuery()
                .ProjectTo<CustomerContractDto>(ObjectMapperApp.Mapper.ConfigurationProvider)
                .ToListAsync(token);

            foreach (var contractHistoryModel in contractHistoryModels)
            {
                var matchingContract = contractModels.FirstOrDefault(x => x.Code == contractHistoryModel.Code);
                if (matchingContract != null)
                {
                    var firstDocumentDetail = contractHistoryModel.CustomerContractDocuments.FirstOrDefault();
                    if (firstDocumentDetail != null)
                    {
                        firstDocumentDetail.DocumentStatus = AppConsts.Expired;
                        matchingContract.CustomerContractDocuments.Add(firstDocumentDetail);
                    }
                    var firstGroupDetail = contractHistoryModel.CustomerContractDocumentGroups.FirstOrDefault();
                    if (firstGroupDetail != null)
                    {
                        firstGroupDetail.DocumentGroupStatus = AppConsts.Expired;
                        matchingContract.CustomerContractDocumentGroups.Add(firstGroupDetail);
                    }
                }
            }
            //var mergedcontractModels = contractModels.Concat(contractHistoryModels).ToList();

            List<Guid> allContractDocumentIds = allContractQuery.SelectMany(main => main.ContractDocumentDetails.Select(doc => doc.DocumentDefinitionId))
                                 .Concat(allContractQuery.SelectMany(main => main.ContractDocumentGroupDetails.Select(docGrup => docGrup.DocumentGroupId)))
                                 .ToList();

            foreach (var model in contractModels)
            {
                //allContractDocumentIds.AddRange(model.CustomerContractDocuments.Select(x => x.Id));

                if (othersOnly)
                {
                    break;
                }

                model.Title = model.Titles.L(inputDto.GetLanguageCode());

                var contractDocuments = model.CustomerContractDocuments;
                var contractDocumentGroups = model.CustomerContractDocumentGroups;

                var customerCompletedDocuments = documents.Where(x => x.Status == core.Enum.ApprovalStatus.Approved).ToList();
                var customerNotCompletedDocuments = documents.Where(x => x.Status != core.Enum.ApprovalStatus.Approved).ToList();

                foreach (var contDocument in contractDocuments)
                {
                    contDocument.Title = contDocument.Titles.L(inputDto.GetLanguageCode());

                    if (customerCompletedDocuments.Exists(x => contDocument.Id == x.DocumentDefinitionId) && contDocument.DocumentStatus != AppConsts.Expired)
                    {
                        contDocument.DocumentStatus = AppConsts.Valid;
                        model.ContractStatus = AppConsts.InProgress;
                    }
                    else if (customerNotCompletedDocuments.Exists(x => contDocument.Id == x.DocumentDefinitionId))
                    {
                        contDocument.DocumentStatus = AppConsts.InProgress;
                    }
                }

                Elastic.Apm.Agent.Tracer.CurrentTransaction.CaptureSpan("For.contractDocumentGroups", ApiConstants.ActionExec, ()
                    =>
                 {
                     foreach (var contractDocGroup in contractDocumentGroups)
                     {
                         contractDocGroup.Title = contractDocGroup.Titles.L(inputDto.GetLanguageCode());

                         int validDocCount = 0;

                         foreach (var groupDocument in contractDocGroup.CustomerContractGroupDocuments)
                         {
                             groupDocument.Title = groupDocument.Titles.L(inputDto.GetLanguageCode());

                             if (customerCompletedDocuments.Exists(x => groupDocument.Id == x.DocumentDefinitionId) && groupDocument.DocumentStatus != AppConsts.Expired)
                             {
                                 groupDocument.DocumentStatus = AppConsts.Valid;
                                 model.ContractStatus = AppConsts.InProgress;
                                 validDocCount++;
                             }
                             else if (customerNotCompletedDocuments.Exists(x => groupDocument.Id == x.DocumentDefinitionId))
                             {
                                 groupDocument.DocumentStatus = AppConsts.InProgress;
                             }
                         }

                         if (contractDocGroup.AtLeastRequiredDocument <= validDocCount)
                         {
                             contractDocGroup.DocumentGroupStatus = AppConsts.Valid;
                         }
                         else if (validDocCount > 0 && validDocCount < contractDocGroup.AtLeastRequiredDocument)
                         {
                             contractDocGroup.DocumentGroupStatus = AppConsts.InProgress;
                         }
                     }
                 });
                bool anyNotValidDocument = model.CustomerContractDocuments.Any(x => x.Required && x.DocumentStatus != AppConsts.Valid);
                bool anyNotValidDocumentGroup = model.CustomerContractDocumentGroups.Any(x => x.Required && x.DocumentGroupStatus != AppConsts.Valid);

                if (!anyNotValidDocument && !anyNotValidDocumentGroup)
                {
                    model.ContractStatus = AppConsts.Valid;
                }

                Elastic.Apm.Agent.Tracer.CurrentTransaction.CaptureSpan("If.Valid.InProgress", ApiConstants.ActionExec, ()
                    =>
                 {
                     if (model.ContractStatus == AppConsts.Valid || model.ContractStatus == AppConsts.InProgress)
                     {
                         SetContractDocumentsMinioUrl(model, documents);
                     }
                 });
            }

            if (String.IsNullOrEmpty(inputDto.Code))
            {
                var otherDocuments = documents.Where(x => !allContractDocumentIds.Distinct().Contains(x.DocumentDefinitionId));

                var otherDocumentDefinition = _dbContext.DocumentDefinition
                                                .IgnoreQueryFilters()
                                                .Where(x => otherDocuments
                                                .Select(x => x.DocumentDefinitionId).Contains(x.Id))
                                                    .AsNoTracking()
                                                    .AsSplitQuery()
                                                    .ProjectTo<CustomerContractDocumentDto>(
                                                        ObjectMapperApp.Mapper.ConfigurationProvider
                                                       ).ToList();

                otherDocumentDefinition.ForEach(x =>
                {
                    x.Title = x.Titles.L(inputDto.GetLanguageCode());
                    x.DocumentStatus = AppConsts.Valid;
                });

                CustomerContractDto contractModel = new CustomerContractDto
                {
                    Code = "Other_idle_docs",
                    Title = inputDto.GetLanguageCode() == "tr-TR" ? "Diğer" : "Other",
                    ContractStatus = "",
                    CustomerContractDocuments = otherDocumentDefinition,
                    CustomerContractDocumentGroups = new List<CustomerContractDocumentGroupDto>()
                };

                SetContractDocumentsMinioUrl(contractModel, documents);

                if (othersOnly)
                {
                    contractModels = new List<CustomerContractDto>
                    {
                        contractModel
                    };
                }
                else
                {
                    contractModels.Add(contractModel);
                }
            }

            return GenericResult<List<CustomerContractDto>>.Success(contractModels);
        }

        private void SetContractDocumentsMinioUrl(CustomerContractDto model, List<DocumentForMinioObject> documentsForMinio)
        {
            List<Guid> contractDocumentDefinitionIds = model.CustomerContractDocuments.Where(x => model.Code == "Other_idle_docs" || x.DocumentStatus == AppConsts.InProgress || x.DocumentStatus == AppConsts.Expired || x.DocumentStatus == AppConsts.Valid).Select(x => x.Id).ToList();

            model.CustomerContractDocumentGroups.ForEach(x =>
            {
                contractDocumentDefinitionIds.AddRange(x.CustomerContractGroupDocuments.Where(x => x.DocumentStatus == AppConsts.InProgress || x.DocumentStatus == AppConsts.Expired || x.DocumentStatus == AppConsts.Valid).Select(x => x.Id).ToList());
            });

            documentsForMinio = documentsForMinio.Where(x => contractDocumentDefinitionIds.Distinct().Contains(x.DocumentDefinitionId)).ToList();

            GetMinioUrl(documentsForMinio);

            model.CustomerContractDocuments.Where(x => documentsForMinio.Select(z => z.DocumentDefinitionId).Contains(x.Id)).ToList().ForEach(x => x.MinioUrl = documentsForMinio.FirstOrDefault(z => z.DocumentDefinitionId == x.Id).MinioUrl);

            foreach (var contractGroup in model.CustomerContractDocumentGroups)
            {
                contractGroup.CustomerContractGroupDocuments.Where(x => documentsForMinio.Select(z => z.DocumentDefinitionId).Contains(x.Id)).ToList().ForEach(x => x.MinioUrl = documentsForMinio.FirstOrDefault(z => z.DocumentDefinitionId == x.Id).MinioUrl);
            }
        }

        private void GetMinioUrl(List<DocumentForMinioObject> minioObjectList)
        {
            minioObjectList.ForEach(x => x.MinioUrl = $"{_baseUrl}{_downloadEndpoint}?ObjectId={x.DocumentContentId}");
        }

        public async Task<GenericResult<List<DocumentCustomerDto>>> GetAllDocuments(GetCustomerDocumentsByContractInputDto inputDto, CancellationToken token)
        {
            var documentsQuery = _dbContext!.Document.Where(x => x.Customer.Reference == inputDto.GetUserReference()).AsQueryable();

            if (inputDto.StartDate.HasValue)
            {
                documentsQuery = documentsQuery.Where(x => x.CreatedAt > inputDto.StartDate.Value);
            }

            if (inputDto.EndDate.HasValue)
            {
                documentsQuery = documentsQuery.Where(x => x.CreatedAt < inputDto.EndDate.Value);
            }

            var documents = await documentsQuery.ToListAsync(token);

            var documentsForMinio = documents.Select(x => new DocumentForMinioObject
            {
                Id = x.Id,
                DocumentDefinitionId = x.DocumentDefinitionId,
                Status = x.Status,
                DocumentContentId = x.DocumentContent.Id.ToString()
            }).ToList();

            GetMinioUrl(documentsForMinio);

            var responseTasks = documents.Select(async x =>
            {
                //var minioUrl = await _minioService.GetDocumentUrl(x.DocumentContent.MinioObjectName, token);
                return new DocumentCustomerDto
                {
                    Code = x.DocumentDefinition.Code,
                    Semver = x.DocumentDefinition.Semver,
                    Status = x.Status.ToString(),
                    MinioUrl = documentsForMinio.Where(z => z.DocumentDefinitionId == x.DocumentDefinitionId).Select(z => z.MinioUrl).FirstOrDefault(),
                    MinioObjectName = x.DocumentContent.MinioObjectName,
                    Reference = x.Customer.Reference
                };
            });
            var response = await Task.WhenAll(responseTasks);
            return GenericResult<List<DocumentCustomerDto>>.Success(response.ToList());
        }

        public async Task<GenericResult<bool>> DeleteAllDocuments(string reference, CancellationToken cts)
        {

            var customerIdsToDelete = await _dbContext.Customer
                                    .Where(c => c.Reference == reference)
                                    .Select(c => c.Id)
                                    .ToListAsync();

            var documentsToDelete = await _dbContext.Document
                                    .Where(d => customerIdsToDelete.Contains(d.CustomerId))
                                    .ToListAsync();

            _dbContext.Document.RemoveRange(documentsToDelete);
            await _dbContext.SaveChangesAsync();
            return GenericResult<bool>.Success(true);


        }

        //private class MinioObject
        //{
        //    public string MinioUrl { get; set; }
        //    public Guid DocumentDefinitionId { get; set; }
        //}

        private class DocumentForMinioObject
        {
            public Guid Id { get; set; }
            public Guid DocumentDefinitionId { get; set; }
            public ApprovalStatus Status { get; set; }
            public string DocumentContentId { get; set; }
            public string MinioUrl { get; set; } = "";
        }
    }
}

