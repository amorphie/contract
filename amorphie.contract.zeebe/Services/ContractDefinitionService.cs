using amorphie.contract.core.Entity.Common;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.zeebe.Model.ContractDefinitionDataModel;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Enum;
using amorphie.contract.application;
using amorphie.contract.core.Model.History;
using amorphie.contract.core.Model;
using amorphie.contract.core.CustomException;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using Newtonsoft.Json;
using System.Diagnostics.Contracts;
using amorphie.contract.application.Contract;
using amorphie.contract.application.Contract.Dto.Input;

namespace amorphie.contract.zeebe.Services
{
    public interface IContractDefinitionService
    {
        Task<ContractDefinition> CreateContractDefinition(ContractDefinitionInputDto inputDto, Guid id);
        Task<ContractDefinition> UpdateContractDefinition(ContractDefinitionInputDto inputDto, Guid id);
    }

    public class ContractDefinitionService : IContractDefinitionService
    {
        private readonly IContractDefinitionAppService _contractDefinitionAppService;

        public ContractDefinitionService(ContractDefinitionAppService contractDefinitionAppService)
        {
            _contractDefinitionAppService = contractDefinitionAppService;
        }

        public async Task<ContractDefinition> CreateContractDefinition(ContractDefinitionInputDto inputDto, Guid id)
        {
            return await _contractDefinitionAppService.CreateContractDefinition(inputDto, id);
        }

        public async Task<ContractDefinition> UpdateContractDefinition(ContractDefinitionInputDto inputDto, Guid id)
        {
            return await _contractDefinitionAppService.UpdateContractDefinition(inputDto, id);
        }
    }
}