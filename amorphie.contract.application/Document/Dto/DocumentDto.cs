using amorphie.contract.core.Enum;
using amorphie.core.Base;

namespace amorphie.contract.application
{
    public class DocumentDto : DtoBase
    {
        public Guid DocumentDefinitionId { get; set; }
        public ApprovalStatus Status { get; set; } = default!;
        public Guid CustomerId { get; set; }
        public DocumentContentDto DocumentContent { get; set; }
        public List<NoteDto> Notes { get; set; }
        public List<MetadataDto> Metadata { get; set; }
    }
}