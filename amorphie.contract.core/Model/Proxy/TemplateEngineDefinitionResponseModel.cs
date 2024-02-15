using System;
namespace amorphie.contract.Models.Proxy
{
    public class TemplateEngineDefinitionResponseModel
    {
        public TemplateEngineDefinitionResponseModel()
        {
        }

        public string Name { get; set; }
        public List<SemanticVersionData> SemanticVersionsData { get; set; }

        public class SemanticVersionData
        {
            public string SemanticVersion { get; set; }
            public List<object> DynamicData { get; set; }
        }

    }
}

