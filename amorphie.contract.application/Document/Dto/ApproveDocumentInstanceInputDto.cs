using System.Text.Json.Serialization;
using amorphie.contract.core.Model;

namespace amorphie.contract.application
{
    public class ApproveDocumentInstanceInputDto
    {
        private HeaderFilterModel _headerModel;

        public Guid DocumentInstanceId { get; set; }
        public Guid? ContractInstanceId { get; set; }
        public string? ContractCode { get; set; }

        [JsonIgnore]
        public HeaderFilterModel HeaderModel
        {
            get => _headerModel ?? throw new InvalidOperationException("HeaderModel is not set.");
            private set => _headerModel = value;
        }

        public void SetHeaderModel(HeaderFilterModel model)
        {
            _headerModel = model ?? throw new ArgumentNullException(nameof(model));
        }
    }

}