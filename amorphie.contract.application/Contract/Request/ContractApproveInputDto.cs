using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model;

namespace amorphie.contract.application.Contract.Request
{
    public class ContractApproveInputDto
    {

        [Required]
        public required string ContractName { get; set; }
        // public Guid ContractInstanceId { get; set; } //bunu ilerde bulundurucaz anyvalid le ilerledigimizden suan kullanılmıyor
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
