using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model;

namespace amorphie.contract.application.Contract.Request
{
    public class ContractInstanceStateInputDto
    {
        [Required]
        public required string ContractName { get; set; }

        public string? Reference { get; private set; }

        public string? LangCode { get; private set; }

        public EBankEntity? EBankEntity { get; private set; }

        public void SetHeaderParameters(HeaderFilterModel headerFilterModel)
        {
            Reference = headerFilterModel.UserReference;
            EBankEntity = headerFilterModel.EBankEntity;
            LangCode = headerFilterModel.LangCode;
        }
    }

}
