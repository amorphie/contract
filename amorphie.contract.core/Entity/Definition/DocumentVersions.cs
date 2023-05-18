using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Definition
{
    [Table("DocumentVersions", Schema = "Definition")]
    public class DocumentVersions:EntityBase
    {
        public string Name {get;set;}
        [ForeignKey("DocumentDefinitionId")]
        public DocumentDefinition DocumentDefinition {get;set;}
    }
}