namespace amorphie.contract.application
{
    public class DocumentInstanceInputDto
    {
        public DocumentContentDto DocumentContent { get; set; }
        public string ContextType { get; set; } = default!;
        public Guid Id { get; set; }
        public string DocumentCode { get; set; }
        public string DocumentVersion { get; set; }
        public string? Reference { get; private set; }
        public string? Owner { get; private set; }
        public long? CustomerNo { get; private set; }
        public List<MetadataDto>? InstanceMetadata { get; set; }
        public List<NoteDto>? Notes { get; set; }

        public void SetHeaderParameters(string userReference, long? customerNo)
        {
            Reference = userReference;
            Owner = userReference;
            CustomerNo = customerNo;
        }
        public override string ToString()
        {
            return Id.ToString() + "##" + Reference + "##" + DocumentCode + "##" + DocumentVersion + "##" + DocumentContent.FileName;
        }
    }
}