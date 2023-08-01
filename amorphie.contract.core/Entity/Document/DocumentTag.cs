using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentTag", Schema = "Doc")]
    public class DocumentTag : EntityBase
    {
        public Guid DocumentDefinitionId { get; set; }

        public DocumentDefinition? DocumentDefinition { get; set; }
        public string Code { get; set; }
        public string Contact { get; set; }

    }
}