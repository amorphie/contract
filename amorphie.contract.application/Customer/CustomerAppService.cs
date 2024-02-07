using System;
using System.Linq;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.Customer.Dto;
using amorphie.contract.application.Customer.Request;
using amorphie.contract.application.Extensions;
using amorphie.contract.data.Contexts;
using amorphie.contract.data.Services;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using static amorphie.contract.application.Customer.CustomerAppService;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Elastic.Apm.Api;

namespace amorphie.contract.application.Customer
{
    public interface ICustomerAppService
    {
        Task<List<CustomerContractDto>> GetDocumentsByContracts(GetCustomerDocumentsByContractInputDto inputDto, CancellationToken token);
        Task<List<DocumentObject>> GetAllDocuments(GetCustomerDocumentsByContractInputDto inputDto, CancellationToken token);
    }

    public class CustomerAppService : ICustomerAppService
    {
        private readonly ProjectDbContext _dbContext;
        private readonly IDocumentService _documentService;

        public CustomerAppService(ProjectDbContext dbContext, IDocumentService documentService)
        {
            _dbContext = dbContext;
            _documentService = documentService;
        }

        public async Task<List<CustomerContractDto>> GetDocumentsByContracts(GetCustomerDocumentsByContractInputDto inputDto, CancellationToken token)
        {
            var contractQuery = _dbContext!.ContractDefinition.AsQueryable();
            var documentQuery = _dbContext.Document.Where(x => x.Customer.Reference == inputDto.Reference).AsQueryable();
            contractQuery = ContractHelperExtensions.LikeWhere(contractQuery, inputDto.Code);


            if (inputDto.StartDate.HasValue)
            {
                documentQuery = documentQuery.Where(x => x.CreatedAt > inputDto.StartDate.Value);
            }

            if (inputDto.EndDate.HasValue)
            {
                documentQuery = documentQuery.Where(x => x.CreatedAt < inputDto.EndDate.Value);
            }

            var contractModels = await contractQuery.Skip(inputDto.Page * inputDto.PageSize).Take(inputDto.PageSize)
                 .AsNoTracking().AsSplitQuery().ProjectTo<CustomerContractDto>(ObjectMapperApp.Mapper.ConfigurationProvider).ToListAsync(token);
            var documents = await documentQuery.Select(x => new
            {
                x.Id,
                x.DocumentDefinitionId,
                x.Status,
                x.DocumentContent.MinioObjectName
            }).AsSplitQuery().ToListAsync(token);
            //List<ContractDefinitionDto> contractModels = contracts.Select(x => new ContractDefinitionDto
            //{
            //    Id = x.Id,
            //    Code = x.Code,
            //    Status = "inProgress"
            //}).ToList();

            //List<CustomerContractDto> contractModels = ObjectMapperApp.Mapper.Map<List<CustomerContractDto>>(contracts);
            Elastic.Apm.Agent.Tracer.CurrentTransaction.CaptureSpan("For.contractModels", ApiConstants.ActionExec, ()
                =>
             {



                 foreach (var model in contractModels)
                 {
                     var contractDocuments = model.CustomerContractDocuments;
                     var contractDocumentGroups = model.CustomerContractDocumentGroups;

                     var customerCompletedDocuments = documents.Where(x => x.Status == core.Enum.EStatus.Completed).ToList();
                     var customerNotCompletedDocuments = documents.Where(x => x.Status != core.Enum.EStatus.Completed).ToList();

                     contractDocuments.Where(x => customerCompletedDocuments.Exists(z => x.Id == z.DocumentDefinitionId)).ToList()
                         .ForEach(x =>
                         {
                             x.DocumentStatus = AppConsts.Valid;
                             model.ContractStatus = AppConsts.InProgress;
                         });

                     contractDocuments.Where(x => customerNotCompletedDocuments.Exists(z => x.Id == z.DocumentDefinitionId)).ToList()
                         .ForEach(x =>
                         {
                             x.DocumentStatus = AppConsts.InProgress;
                         });
                     Elastic.Apm.Agent.Tracer.CurrentTransaction.CaptureSpan("For.contractDocumentGroups", ApiConstants.ActionExec, ()
                         =>
                      {
                          foreach (var contractDocGroup in contractDocumentGroups)
                          {
                              int validDocCount = 0;
                              contractDocGroup.CustomerContractGroupDocuments.Where(x => customerCompletedDocuments.Exists(z => x.Id == z.DocumentDefinitionId)).ToList()
                     .ForEach(x =>
                     {
                         x.DocumentStatus = AppConsts.Valid;
                         model.ContractStatus = AppConsts.InProgress;
                         validDocCount++;
                     });

                              contractDocGroup.CustomerContractGroupDocuments.Where(x => customerNotCompletedDocuments.Exists(z => x.Id == z.DocumentDefinitionId)).ToList()
                     .ForEach(x =>
                     {
                         x.DocumentStatus = AppConsts.InProgress;
                     });

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
                     bool anyNotValidDocument = model.CustomerContractDocuments.Where(x => x.Required).Any(x => x.DocumentStatus != AppConsts.Valid);
                     bool anyNotValidDocumentGroup = model.CustomerContractDocumentGroups.Where(x => x.Required).Any(x => x.DocumentGroupStatus != AppConsts.Valid);

                     if (!anyNotValidDocument && !anyNotValidDocumentGroup)
                     {
                         model.ContractStatus = AppConsts.Valid;
                     }

                     Elastic.Apm.Agent.Tracer.CurrentTransaction.CaptureSpan("If.Valid.InProgress", ApiConstants.ActionExec, ()
                         =>
                      {

                          if (model.ContractStatus == AppConsts.Valid || model.ContractStatus == AppConsts.InProgress)
                          {
                              var minioDocuments = model.CustomerContractDocuments.Where(x => x.DocumentStatus == AppConsts.InProgress || x.DocumentStatus == AppConsts.Valid).Select(x => new MinioObject
                              {
                                  DocumentDefinitionId = x.Id,
                                  MinioUrl = ""
                              }).ToList();

                              model.CustomerContractDocumentGroups.ForEach(x =>
                              {
                                  minioDocuments.AddRange(x.CustomerContractGroupDocuments.Where(x => x.DocumentStatus == AppConsts.InProgress || x.DocumentStatus == AppConsts.Valid).Select(x => new MinioObject
                                  {
                                      DocumentDefinitionId = x.Id,
                                      MinioUrl = ""
                                  }).ToList());
                              });

                              minioDocuments = minioDocuments.GroupBy(x => x.DocumentDefinitionId).Select(x => x.First()).ToList();

                              minioDocuments.ForEach(async x => x.MinioUrl = await _documentService.GetDocumentPath(
                                  documents.FirstOrDefault(z => z.DocumentDefinitionId == x.DocumentDefinitionId).MinioObjectName, token));

                              model.CustomerContractDocuments.Where(x => minioDocuments.Select(z => z.DocumentDefinitionId).Contains(x.Id)).ToList().ForEach(x => x.MinioUrl = minioDocuments.FirstOrDefault(z => z.DocumentDefinitionId == x.Id).MinioUrl);

                              foreach (var contractGroup in model.CustomerContractDocumentGroups)
                              {
                                  contractGroup.CustomerContractGroupDocuments.Where(x => minioDocuments.Select(z => z.DocumentDefinitionId).Contains(x.Id)).ToList().ForEach(x => x.MinioUrl = minioDocuments.FirstOrDefault(z => z.DocumentDefinitionId == x.Id).MinioUrl);
                              }
                          }
                      });

                 }


             });
            //foreach (var model in contractModels)
            //{
            //    var currentContractDocumentDetails = contracts.Where(x => x.Id == model.Id).FirstOrDefault().ContractDocumentDetails;
            //    var currentContractDocumentGroupDetails = contracts.Where(x => x.Id == model.Id).FirstOrDefault().ContractDocumentGroupDetails;

            //    model.CustomerContractDocuments = ObjectMapperApp.Mapper.Map<List<CustomerContractDocumentDto>>(currentContractDocumentDetails.Select(x => x.DocumentDefinition));
            //    model.CustomerContractDocumentGroups = ObjectMapperApp.Mapper.Map<List<CustomerContractDocumentGroupDto>>(currentContractDocumentGroupDetails.Select(x => x.DocumentGroup));

            //    model.CustomerContractDocuments.ForEach(x => {
            //        x.Valid = true;
            //        x.Required = currentContractDocumentDetails.FirstOrDefault(z => z.DocumentDefinitionId == x.Id).Required;
            //    });

            //}

            return contractModels;
        }

        private class MinioObject
        {
            public string MinioUrl { get; set; }
            public Guid DocumentDefinitionId { get; set; }
        }

        public async Task<List<DocumentObject>> GetAllDocuments(GetCustomerDocumentsByContractInputDto inputDto, CancellationToken token)
        {
            var documentsQuery = _dbContext!.Document.Where(x => x.Customer.Reference == inputDto.Reference).AsQueryable();

            //documentsQuery = ContractHelperExtensions.LikeWhere(documentsQuery, inputDto.Code);

            if (inputDto.StartDate.HasValue)
            {
                documentsQuery = documentsQuery.Where(x => x.CreatedAt > inputDto.StartDate.Value);
            }

            if (inputDto.EndDate.HasValue)
            {
                documentsQuery = documentsQuery.Where(x => x.CreatedAt < inputDto.EndDate.Value);
            }

            IDocumentService documentService = new DocumentService();
            var documents = await documentsQuery.Skip(inputDto.Page * inputDto.PageSize).Take(inputDto.PageSize).ToListAsync(token);

            var responseTasks = documents.Select(async x =>
            {
                var minioUrl = await documentService.GetDocumentPath(x.DocumentContent.MinioObjectName, token);
                return new DocumentObject
                {
                    Code = x.DocumentDefinition.Code,
                    Semver = x.DocumentDefinition.Semver,
                    Status = x.Status.ToString(),
                    MinioUrl = minioUrl,
                    MinioObjectName = x.DocumentContent.MinioObjectName,
                    Reference = x.Customer.Reference
                };
            });

            return (await Task.WhenAll(responseTasks)).ToList();
        }

        public class DocumentObject
        {
            public string Code { get; set; }
            public string Semver { get; set; }
            public string Status { get; set; }
            public string MinioUrl { get; set; }
            public string MinioObjectName { get; set; }
            public string Reference { get; set; }
        }
    }
}

