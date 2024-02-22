using System.Text.Json.Serialization;

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
        public string Reference { get; set; }
        public string Owner { get; set; }

        public List<EntityPropertyDto>? EntityPropertyDtos { get; set; }
        public override string ToString()
        {
            return Id.ToString() + "##" + Reference + "##" + DocumentCode + "##" + DocumentVersion + "##" + FileName;
        }
    }
}