namespace amorphie.contract.core.Model.Minio;

public class GetMinioObjectModel
{
    public string FileName { get; init; } = null!;

    public string ContentType { get; init; } = null!;

    public string FileContent { get; set; } = null!;

}