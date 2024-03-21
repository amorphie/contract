namespace amorphie.contract.application.Contract.Dto
{

    public class DocumentInstanceDetailDto
    {
        public DocumentInstanceOnlineSingDto OnlineSing { get; set; } = new DocumentInstanceOnlineSingDto();
    }
    public class DocumentInstanceOnlineSingDto
    {
        public string TemplateCode { get; set; }
        public string Version { get; set; }

    }
}
