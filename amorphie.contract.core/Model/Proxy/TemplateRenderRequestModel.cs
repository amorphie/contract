﻿using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
namespace amorphie.contract.core.Model.Proxy
{
    public class TemplateRenderRequestModel
    {
        public TemplateRenderRequestModel()
        {
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
        // [Newtonsoft.Json.JsonProperty("item-id")]
        // [JsonPropertyName("item-id")]
        // public string ItemId { get; set; }
        // public string Action { get; set; }
        public string Identity { get; set; }
        // public string Customer { get; set; }
        // [Newtonsoft.Json.JsonProperty("children-name")]
        // [JsonPropertyName("children-name")]
        // public string ChildrenName { get; set; }
        // public List<object> Children { get; set; }

    }

}

