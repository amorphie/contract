using System;
using Newtonsoft.Json;

namespace amorphie.contract.RequestModel.Proxy
{
	public class TemplateRenderRequestModel
	{
		public TemplateRenderRequestModel()
		{
		}

		public string Name { get; set; }
        [JsonProperty("render-id")]
        public Guid RenderId { get; set; }
        [JsonProperty("render-data")]
        public object RenderData { get; set; }
        [JsonProperty("render-data-for-log")]
        public object RenderDataForLog { get; set; }
        [JsonProperty("semantic-version")]
        public string SemanticVersion { get; set; }
        [JsonProperty("process-name")]
        public string ProcessName { get; set; }
        [JsonProperty("item-id")]
        public string ItemId { get; set; }
		public string Action { get; set; }
		public string Identity { get; set; }
		public string Customer { get; set; }
        [JsonProperty("children-name")]
        public string ChildrenName { get; set; }
		public List<object> Children { get; set; }
	}
}

