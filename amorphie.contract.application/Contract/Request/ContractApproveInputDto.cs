using System.ComponentModel.DataAnnotations;

namespace amorphie.contract.application.Contract.Request
{
    public class ContractApprovedAndPendingDocumentsInputDto : BaseHeader
    {

        [Required]
        public required string ContractCode { get; set; }
        public Guid ContractInstanceId { get; set; } //bunu ilerde bulundurucaz anyvalid le ilerledigimizden suan kullanılmıyor
      
    }

}
