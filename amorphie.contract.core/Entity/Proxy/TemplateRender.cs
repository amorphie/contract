using System;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Proxy
{
    public class TemplateRender : EntityBase
    {
        public TemplateRender()
        {
        }

        public string TemplateName { get; set; }
        public string RenderData { get; set; }
        public string RenderType { get; set; }
    }
}

