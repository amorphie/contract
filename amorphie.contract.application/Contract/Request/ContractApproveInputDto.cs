using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model;

namespace amorphie.contract.application.Contract.Request
{
    public class ContractApprovedAndPendingDocumentsInputDto : BaseHeader
    {

        [Required]
        public required string ContractName { get; set; }
        public Guid ContractInstanceId { get; set; } //bunu ilerde bulundurucaz anyvalid le ilerledigimizden suan kullanılmıyor
      
    }

}
