using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.application.TemplateEngine.Dto
{
    public class TemplateRenderTagInputDto
    {
        public string DomainName { get; set; }
        public string TagName { get; set;}
        public string EntityName { get; set; }
        public string ViewTemplateName { get; set; }
        public string Reference { get; set; }
        public string Version { get; set; }
    }
}