using amorphie.contract.core.Entity.Common;

namespace amorphie.contract.application
{

    public class RootDocumentDto : BaseDto
    {
        public required string DocumentDefinitionId { get; set; }
        public required DocumentDefinitionDto DocumentDefinition { get; set; }
    public DocumentContentDto DocumentContent { get; set; }
    public required string StatuCode { get; set; }
    public DateTime CreatedAt { get; set; }
}


}