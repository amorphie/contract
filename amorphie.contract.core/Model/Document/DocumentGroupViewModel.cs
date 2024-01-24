using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Model.Document
{
    public class DocumentGroupViewModel 
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Status { get; set; }
        public Guid Id { get; set; }
        public List<MultilanguageText>? MultilanguageText { get; set; }
        //TODO: mapping
        // public List<DocumentDefinitionViewModel>? DocumentDefinitionList { get; set; }
    }
}