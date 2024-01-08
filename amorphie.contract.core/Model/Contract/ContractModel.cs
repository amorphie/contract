using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using amorphie.contract.core.Model.Document;

namespace amorphie.contract.core.Model.Contract
{


    public class AllowedFormatModel
    {
        public string Format { get; set; }
        [JsonPropertyName("max-size-kilobytes")]
        public int MaxSizeKilobytes { get; set; }
    }

    public class Upload
    {
        [JsonPropertyName("sca-required")]
        public bool ScaRequired { get; set; }
        [JsonPropertyName("allowed-clients")]
        public List<string> AllowedClients { get; set; }
        [JsonPropertyName("allowed-formats")]
        public List<AllowedFormatModel> AllowedFormats { get; set; }
    }

    public class OnlineSignModel
    {

        [JsonPropertyName("sca-required")]
        public bool ScaRequired { get; set; }
        [JsonPropertyName("allowed-clients")]
        public List<string> AllovedClients { get; set; }
        [JsonPropertyName("document-model-template")]
        public List<DocumentModelTemplate> DocumentModelTemplate { get; set; }

    }
    public class DocumentModelTemplate
    {
        public string Name { get; set; }
        [JsonPropertyName("min-version")]
        public string MinVersion { get; set; }
    }

    public class DocumentModel
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public List<MultilanguageTextModel> MultilanguageText { get; set; }
        public string Status { get; set; }
        public bool Required { get; set; }
        public Upload Upload { get; set; }
        public bool Render { get; set; }
        [JsonPropertyName("online-sign")]
        public OnlineSignModel OnlineSign { get; set; }
        public string Version { get; set; }
    }

    public class DocumentGroupModel
    {
        public bool Required { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        [JsonPropertyName("at-least-required-document")]
        public int AtLeastRequiredDocument { get; set; }
        public List<DocumentModel> Document { get; set; }
    }

    public class ContractModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public List<DocumentModel> Document { get; set; }
        public List<DocumentGroupModel> DocumentGroups { get; set; }
    }
}