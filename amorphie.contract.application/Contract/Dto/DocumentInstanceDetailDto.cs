namespace amorphie.contract.application.Contract.Dto
{

    public class DocumentInstanceDetailDto
    {
        public DocumentInstanceOnlineSignDto OnlineSign { get; set; } = new DocumentInstanceOnlineSignDto();
    }
    public class DocumentInstanceOnlineSignDto
    {
        public string TemplateCode { get; set; }
        public string Version { get; set; }

    }
}
