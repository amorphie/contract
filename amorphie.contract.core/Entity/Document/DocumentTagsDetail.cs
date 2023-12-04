using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentTag", Schema = "Doc")]

    public class DocumentTagsDetail : AudiEntity
    {
        [Required]
        public Guid DocumentDefinitionId { get; set; }
        [Required]

        public Guid TagId { get; set; }
        public Common.Tag Tags { get; set; }

    }
}