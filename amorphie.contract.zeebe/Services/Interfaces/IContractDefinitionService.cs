using System.Diagnostics.Contracts;
using amorphie.contract.core.Entity.Contract;
namespace amorphie.contract.zeebe.Services.Interfaces
{
    public interface IContractDefinitionService
    {
        Task<ContractDefinition> DataModelToContractDefinition(dynamic contractDefinitionDataDynamic, Guid id);
    }
}