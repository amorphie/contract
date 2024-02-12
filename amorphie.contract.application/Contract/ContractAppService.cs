﻿using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application.Contract.Request;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Enum;
using amorphie.contract.data.Contexts;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Core.Operations;

namespace amorphie.contract.application.Contract
{
    public interface IContractAppService
    {

        Task<ContractDefinitionDto> Instance(ContractInstanceInputDto req, CancellationToken cts);
        Task<bool> InstanceState(ContractInstanceInputDto req, CancellationToken cts);

        Task<bool> GetExist(ContractGetExistInputDto req, CancellationToken cts);

    }
    public class ContractAppService : IContractAppService
    {
        private readonly ProjectDbContext _dbContext;
        public ContractAppService(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<bool> GetExist(ContractGetExistInputDto req, CancellationToken cts)
        {
            var contractDefinition = await _dbContext.ContractDefinition
                .AnyAsync(x => x.Code == req.Code && x.BankEntity == req.EBankEntity, cts);
            return contractDefinition;
        }

        public async Task<ContractDefinitionDto> Instance(ContractInstanceInputDto req, CancellationToken cts)
        {
            //TODO: Daha sonra eklenecek && x.BankEntity == req.EBankEntity
            var contractDefinition = await _dbContext.ContractDefinition.FirstOrDefaultAsync(x => x.Code == req.ContractName, cts);
            // var ss = await _dbContext.ContractDefinition.Include(x=>x.ContractDocumentDetails).FirstOrDefaultAsync(x => x.Code == req.ContractName, cts);
            if (contractDefinition == null)
            {
                return new ContractDefinitionDto { Status = "not contract" };
            }

            var documentList = contractDefinition.ContractDocumentDetails
                .Select(x => x.DocumentDefinitionId)
                .ToList();

            var documentGroupList = contractDefinition.ContractDocumentGroupDetails
                .SelectMany(x => x.DocumentGroup.DocumentGroupDetails)
                .Select(a => a.DocumentDefinitionId)
                .ToList();

            var customerDocument = await _dbContext.Document
                .Where(x => x.Customer.Reference == req.Reference && documentList.Contains(x.DocumentDefinitionId))
                .Select(x => x.DocumentDefinitionId)
                .ToListAsync(cts);

            var customerDocumentGroup = await _dbContext.Document
                .Where(x => x.Customer.Reference == req.Reference && documentGroupList.Contains(x.DocumentDefinitionId))
                .Select(x => x.DocumentDefinitionId)
                .ToListAsync(cts);

            var listDocument = contractDefinition.ContractDocumentDetails
                .Where(d => !customerDocument.Contains(d.DocumentDefinitionId));

            var listDocumentGroup = contractDefinition.ContractDocumentGroupDetails.ToList();

            var contractDocumentDetails = ObjectMapperApp.Mapper.Map<List<ContractDocumentDetailDto>>(listDocument);

            var contractModel = new ContractDefinitionDto
            {
                Status = EStatus.InProgress.ToString(),
                Id = contractDefinition.Id,
                Code = contractDefinition.Code,
                ContractDocumentDetails = contractDocumentDetails,
                ContractDocumentGroupDetails = ObjectMapperApp.Mapper.Map<List<ContractDocumentGroupDetailDto>>(listDocumentGroup)
            };

            if (contractModel.ContractDocumentDetails.Count == 0)
                contractModel.Status = EStatus.Completed.ToString();

            return contractModel;
        }
        public async Task<bool> InstanceState(ContractInstanceInputDto req, CancellationToken cts)
        {
            //TODO: Daha sonra eklenecek && x.BankEntity == req.EBankEntity
            var contractDefinition = await _dbContext.ContractDefinition.FirstOrDefaultAsync(x => x.Code == req.ContractName, cts);
            // var ss = await _dbContext.ContractDefinition.Include(x=>x.ContractDocumentDetails).FirstOrDefaultAsync(x => x.Code == req.ContractName, cts);
            if (contractDefinition == null)
            {
                return false;
            }

            var documentList = contractDefinition.ContractDocumentDetails
                .Select(x => x.DocumentDefinitionId)
                .ToList();

            var documentGroupList = contractDefinition.ContractDocumentGroupDetails
                .SelectMany(x => x.DocumentGroup.DocumentGroupDetails)
                .Select(a => a.DocumentDefinitionId)
                .ToList();

            var customerDocument = await _dbContext.Document
                .Where(x => x.Customer.Reference == req.Reference && documentList.Contains(x.DocumentDefinitionId))
                .Select(x => x.DocumentDefinitionId)
                .ToListAsync(cts);

            var customerDocumentGroup = await _dbContext.Document
                .Where(x => x.Customer.Reference == req.Reference && documentGroupList.Contains(x.DocumentDefinitionId))
                .Select(x => x.DocumentDefinitionId)
                .ToListAsync(cts);

            var listDocument = contractDefinition.ContractDocumentDetails
                .Where(d => !customerDocument.Contains(d.DocumentDefinitionId));

            var listDocumentGroup = contractDefinition.ContractDocumentGroupDetails.ToList();

            var contractDocumentDetails = ObjectMapperApp.Mapper.Map<List<ContractDocumentDetailDto>>(listDocument);

            var contractModel = new ContractDefinitionDto
            {
                Status = EStatus.InProgress.ToString(),
                Id = contractDefinition.Id,
                Code = contractDefinition.Code,
                ContractDocumentDetails = contractDocumentDetails,
                ContractDocumentGroupDetails = ObjectMapperApp.Mapper.Map<List<ContractDocumentGroupDetailDto>>(listDocumentGroup)
            };

            if (contractModel.ContractDocumentDetails.Count == 0)
                return true;

            return false;
        }
    }
}
