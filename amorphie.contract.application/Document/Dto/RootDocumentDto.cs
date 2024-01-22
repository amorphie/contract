namespace amorphie.contract.application
{

    public class RootDocumentDto : BaseDto
    {
        public string DocumentDefinitionId { get; set; }
        public DocumentDefinitionDto DocumentDefinition { get; set; }
        public DocumentContentDto DocumentContent { get; set; }
        public string StatuCode { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}