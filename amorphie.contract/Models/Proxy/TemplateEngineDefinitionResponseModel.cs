using System;
namespace amorphie.contract.Models.Proxy
{
    public class TemplateEngineDefinitionResponseModel
    {
        public TemplateEngineDefinitionResponseModel()
        {
        }

        public string Name { get; set; }
        public List<string> SemanticVersions { get; set; }
    }
}

