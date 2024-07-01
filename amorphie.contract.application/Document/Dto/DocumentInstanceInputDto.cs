using System.ComponentModel.DataAnnotations;
using amorphie.contract.application.Contract.Dto.Zeebe;

namespace amorphie.contract.application
{
    public class DocumentInstanceInputDto : BaseHeader
    {
        public DocumentContentDto DocumentContent { get; set; }

        public Guid? ContractInstanceId { get; set; }
        public string? ContractCode { get; set; }

        [Required]
        public required Guid RenderId { get; set; }

        public string DocumentCode { get; set; }
        public string DocumentVersion { get; set; }
        public long? CustomerNo { get; private set; }
        public List<MetadataDto>? InstanceMetadata { get; set; }
        public List<NoteDto>? Notes { get; set; }

        public ContractWithoutHeaderDto? ContractWithoutHeader { get; set; }

        // public override string ToString()
        // {
        //     return Id.ToString() + "##" + Reference + "##" + DocumentCode + "##" + DocumentVersion + "##" + DocumentContent.FileName;
        // }
    }

}