using System.Text.Json.Serialization;
using amorphie.contract.core.Model;

namespace amorphie.contract.application
{
    public class DocumentInstanceInputDto
    {
        public Guid Id { get; set; }
        [JsonPropertyName("file-type")]
        public string FileType { get; set; }
        [JsonPropertyName("file-byte-array")]
        private byte[] fileByteArray { get; set; }
        public string FileContext { get; set; }
        public string FileContextType { get; set; }
        [JsonPropertyName("file-name")]
        public string FileName { get; set; }
        public string DocumentCode { get; set; }
        public string DocumentVersion { get; set; }
        public string? Reference { get; private set; }
        public string? Owner { get; private set; }
        public void SetHeaderParameters(HeaderFilterModel headerFilterModel)
        {
            Reference = headerFilterModel.UserReference;
            Owner = headerFilterModel.UserReference;
        }
        public void SetHeaderParameters(string userReference)
        {
            Reference = userReference;
            Owner = userReference;
        }
        public override string ToString()
        {
            return Id.ToString() + "##" + Reference + "##" + DocumentCode + "##" + DocumentVersion + "##" + FileName;
        }
    }
}