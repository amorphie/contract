using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.application.TemplateEngine.Dto
{
    public class RenderResponseDto
    {
        public TemplateRenderTagInputDto TemplateRenderRequestModel { get; set; }
        public string Content { get; set; }
    }
}