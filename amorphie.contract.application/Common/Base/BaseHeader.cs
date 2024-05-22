using System.Text.Json.Serialization;
using amorphie.contract.core.Model;

namespace amorphie.contract.application;

public class BaseHeader
{
    private HeaderFilterModel _headerModel;

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