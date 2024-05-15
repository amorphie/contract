using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Extensions;

namespace amorphie.contract.core.Model.Minio;

public class UploadFileModel
{
    [Required]
    public required string FileName { get; set; }
    [Required]
    public required byte[] Data { get; set; }
    [Required]
    public required string ContentType { get; set; }
    [Required]
    public required string DocumentDefinitionCode { get; set; }
    public string ContractDefinitionCode { get; set; }
    [Required]
    public required string DocumentDefinitionVersion { get; set; }
    [Required]
    public required string Reference { get; set; }
    [Required]
    public required ApprovalStatus ApprovalStatus { get; set; }
    public MemoryStream MemoryStream
    {
        get
        {

            if (!Data.Any())
            {
                throw new ArgumentNullException($"Filename = {FileName} Reference = {Reference} DocumentDefinitionCode= {DocumentDefinitionCode} Data cannot be null");
            }
            return new MemoryStream(Data);
        }
    }
    public string ObjectName
    {
        get
        {
            return $"{DocumentDefinitionCode}/{Reference}_{DocumentDefinitionVersion}_{FileName}{FileExtension.GetFileExtensionFromMimeType(ContentType)}".Trim();
        }
    }
    public Dictionary<string, string> MetaDataHeader
    {
        get
        {
            var metadata = new Dictionary<string, string>
            {
                { nameof(DocumentDefinitionCode), DocumentDefinitionCode },
                { nameof(DocumentDefinitionVersion), DocumentDefinitionVersion },
                { nameof(Reference), Reference },
                { nameof(ApprovalStatus), ApprovalStatus.ToString() }
            };
            if (!string.IsNullOrEmpty(ContractDefinitionCode))
                metadata[nameof(ContractDefinitionCode)] = ContractDefinitionCode;


            return metadata;
        }
    }
}