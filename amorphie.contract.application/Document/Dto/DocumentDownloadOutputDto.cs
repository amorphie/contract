namespace amorphie.contract.application;

public class DocumentDownloadOutputDto
{
    public string FileName { get; init; } = null!;

    public string FileType { get; init; } = null!;

    public string FileContent { get; set; } = null!;
}