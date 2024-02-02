using System;
using System.Linq;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.Customer.Dto;
using amorphie.contract.application.Customer.Request;
using amorphie.contract.application.Extensions;
using amorphie.contract.data.Contexts;
using amorphie.contract.data.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace amorphie.contract.application.Customer
{
	public interface ICustomerAppService
	{
        Task<List<CustomerContractDto>> GetDocumentsByContracts(GetCustomerDocumentsByContractInputDto inputDto, CancellationToken token);
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

            var contracts = await contractQuery.Skip(inputDto.Page * inputDto.PageSize).Take(inputDto.PageSize).ToListAsync(token);
            var documents = await documentQuery.ToListAsync(token);

            //List<ContractDefinitionDto> contractModels = contracts.Select(x => new ContractDefinitionDto
            //{
            //    Id = x.Id,
            //    Code = x.Code,
            //    Status = "inProgress"
            //}).ToList();

            List<CustomerContractDto> contractModels = ObjectMapperApp.Mapper.Map<List<CustomerContractDto>>(contracts);

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

                bool anyNotValidDocument = model.CustomerContractDocuments.Where(x => x.Required).Any(x => x.DocumentStatus != AppConsts.Valid);
                bool anyNotValidDocumentGroup = model.CustomerContractDocumentGroups.Where(x => x.Required).Any(x => x.DocumentGroupStatus != AppConsts.Valid);

                if (!anyNotValidDocument && !anyNotValidDocumentGroup)
                {
                    model.ContractStatus = AppConsts.Valid;
                }

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

                    minioDocuments.ForEach(x => x.MinioUrl = _documentService.GetDocumentPath(
                        documents.FirstOrDefault(z => z.DocumentDefinitionId == x.DocumentDefinitionId).DocumentContent.MinioObjectName, token).GetAwaiter().GetResult());

                    model.CustomerContractDocuments.Where(x => minioDocuments.Select(z => z.DocumentDefinitionId).Contains(x.Id)).ToList().ForEach(x => x.MinioUrl = minioDocuments.FirstOrDefault(z => z.DocumentDefinitionId == x.Id).MinioUrl);

                    foreach (var contractGroup in model.CustomerContractDocumentGroups)
                    {
                        contractGroup.CustomerContractGroupDocuments.Where(x => minioDocuments.Select(z => z.DocumentDefinitionId).Contains(x.Id)).ToList().ForEach(x => x.MinioUrl = minioDocuments.FirstOrDefault(z => z.DocumentDefinitionId == x.Id).MinioUrl);
                    }
                }
            }


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
    }

    
}

