using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.data.Entity.Base;

namespace amorphie.contract.data.Entity.Definition
{
    [Table("DocumentType", Schema = "Definition")]
    public class DocumentDefinitionType : BaseEntity
    {
        [Key]
        [MaxLength(36)]
        public Guid DocumentDefinitionId { get; set; }

        public string Name { get; set; }
        public string ContentType { get; set; }


    }
}