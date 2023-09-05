using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentTag", Schema = "Doc")]
    [Index(nameof(Code), IsUnique = true)]

    public class DocumentTag : EntityBase
    {
        public Guid DocumentDefinitionId { get; set; }

        // public DocumentDefinition? DocumentDefinition { get; set; }
        public string Code { get; set; }
        public string Contact { get; set; }

    }
}