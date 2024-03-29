using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
namespace amorphie.contract.zeebe.Model
{

    public class DocumentDef
    {
        public string DocumentDefinitionCode { get; set; }
        public string DocumentSemanticVersion { get; set; }
    }
    public class ApprovedDocument : DocumentDef
    {

        public Guid ContractInstanceId { get; set; }
        public Guid RenderId { get; set; }
        public bool Approved { get; set; }
    }
    public class ApprovedDocumentList
    {
        public ApprovedDocumentList()
        {
            Document = new List<ApprovedDocument>();
        }
        public List<ApprovedDocument> Document { get; set; }

    }

    public class ApprovedTemplateDocumentList
    {
        public string DocumentDefinitionCode { get; set; }
        public string DocumentSemanticVersion { get; set; }
        public Guid ContractInstanceId { get; set; }

        public bool Approved { get; set; }
        public string Name { get; set; }
        public Guid RenderId { get; set; }
        public object RenderData { get; set; }
        public object RenderDataForLog { get; set; }
        public string SemanticVersion { get; set; }
        public string ProcessName { get; set; }
        public string Identity { get; set; }

    }
    public class TemplateRenderRequestModel
    {
        public TemplateRenderRequestModel()
        {
        }
        public TemplateRenderRequestModel(ApprovedTemplateDocumentList approvedDocumentList)
        {
            RenderId = approvedDocumentList.RenderId;
            RenderData = approvedDocumentList.RenderData;
            RenderDataForLog = approvedDocumentList.RenderDataForLog;
            SemanticVersion = approvedDocumentList.SemanticVersion;
            ProcessName = approvedDocumentList.ProcessName;
            Identity = approvedDocumentList.Identity;
            Name = approvedDocumentList.Name;
        }

        public string Name { get; set; }
        [Newtonsoft.Json.JsonProperty("render-id")]
        [JsonPropertyName("render-id")]
        public Guid RenderId { get; set; }
        [Newtonsoft.Json.JsonProperty("render-data")]
        [JsonPropertyName("render-data")]
        public object RenderData { get; set; }
        [Newtonsoft.Json.JsonProperty("render-data-for-log")]
        [JsonPropertyName("render-data-for-log")]
        public object RenderDataForLog { get; set; }
        [Newtonsoft.Json.JsonProperty("semantic-version")]
        [JsonPropertyName("semantic-version")]
        public string SemanticVersion { get; set; }
        [Newtonsoft.Json.JsonProperty("process-name")]
        [JsonPropertyName("process-name")]
        public string ProcessName { get; set; }
        public string Identity { get; set; }
    }
}

