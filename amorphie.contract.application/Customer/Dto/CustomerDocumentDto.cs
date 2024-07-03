using System.Text.Json.Serialization;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.core.Enum;

namespace amorphie.contract.application.Customer.Dto
{
    public class CustomerDocumentDto
    {
        public Guid? Id { get; set; }
        public Guid? DocumentDefinitionId { get; set; }
        public ApprovalStatus? Status { get; set; }
        public Guid? DocumentContentId { get; set; }
        public DateTime? CreatedAt { get; set; }
        //DocDef İçin Opsiyonel
        public string? Code { get; set; }
        public string? Version { get; set; }

    }

}

