namespace amorphie.contract.application.Contract.Dto
{

  public class DocumentInstanceDetailDto
  {
    public DocumentInstanceOnlineSingDto OnlineSing { get; set; } = new DocumentInstanceOnlineSingDto();
    public DocumentInstanceUploadDto UploadDto { get; set; } = new DocumentInstanceUploadDto();
  }
  public class DocumentInstanceOnlineSingDto
  {
    public string TemplateCode { get; set; }

  }
  public class DocumentInstanceUploadDto
  {
    public string TemplateCode { get; set; }

  }
}
