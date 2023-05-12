using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.data.Entity.Base;

namespace amorphie.contract.data.Entity.Definition
{
    [Table("DocumentTemplate", Schema = "Definition")]

    public class DocumentDefinitionTemplate : BaseEntity
    {
        [Key]
        [MaxLength(36)]
        public Guid DocumentDefinitionId { get; set; }
        [Key]
        [MaxLength(36)]
        public Guid LanguageId { get; set; }
        public string Name { get; set; }

    }
}