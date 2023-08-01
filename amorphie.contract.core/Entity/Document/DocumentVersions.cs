using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentVersions", Schema = "Doc")]
    public class DocumentVersions:EntityBase
    {
        public string Name {get;set;}
        public Guid DocumentDefinitionId { get; set; }

        public DocumentDefinition? DocumentDefinition {get;set;}
    }
}