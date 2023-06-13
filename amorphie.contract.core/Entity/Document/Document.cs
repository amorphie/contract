using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("Document", Schema = "Doc")]
    public class Document : EntityBase
    {
        
        public Guid DocumentDefinitionId { get; set; }
        public DocumentDefinition DocumentDefinition { get; set; }
        public Guid DocumentContentId{ get; set; }

        public DocumentContent DocumentContent { get; set; }
        public string Note  { get; set; }
        public bool ManuelControl  { get; set; }
    }
}