using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.application
{

    public class DocumentGroupDto : BaseDto
    {
        public DocumentDefinitionDto DocumentDefinition { get; set; } = default!;

        public string Status { get; set; } = default!;

        public List<MultilanguageText> MultilanguageText { get; set; } = default!;

    }
}