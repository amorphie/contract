using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model;

namespace amorphie.contract.application.Contract.Request
{
    public class ContractInstanceInputDto
    {

        [Required]
        public required string ContractName { get; set; }

        public required Guid ContractInstanceId { get; set;}
        public string? Reference { get; private set; }
        public string? LangCode { get; private set; }

        public EBankEntity? EBankEntity { get; private set; }
        public void SetHeaderParameters(string userReference)
        {
            Reference = userReference;
        }
        public void SetHeaderParameters(HeaderFilterModel headerFilterModel)
        {
            Reference = headerFilterModel.UserReference;
            LangCode = headerFilterModel.LangCode;
            EBankEntity = headerFilterModel.EBankEntity;
        }
    }

}
