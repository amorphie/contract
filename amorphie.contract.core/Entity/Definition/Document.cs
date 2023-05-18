using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Definition
{
    [Table("Document", Schema = "Definition")]
    public class Document : EntityBase
    {
        [ForeignKey("DocumentDefinitionId")]
        public DocumentDefinition DocumentDefinition { get; set; }
        [ForeignKey("DocumentContentId")]
        public DocumentContent DocumentContent { get; set; }
    }
}