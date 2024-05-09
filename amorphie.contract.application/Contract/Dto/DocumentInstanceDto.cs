using System.Text.Json.Serialization;

namespace amorphie.contract.application.Contract.Dto
{

    public class DocumentInstanceDto
    {
        [JsonIgnore]
        public Guid? DocumentInstanceId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Code { get; set; }
        public string? UseExisting { get; set; }
        public string? MinVersion { get; set; }
        public bool IsRequired { get; set; }

        public string? LastVersion { get; set; }

        [JsonIgnore]
        public bool IsSigned { get; set; }

        public void Sign()
        {
            IsSigned = true;
        }

        public DocumentInstanceDetailDto DocumentDetail { get; set; } = new DocumentInstanceDetailDto();

    }
}
