using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentFormatDetail", Schema = "Doc")]

    public class DocumentFormatDetail : EntityBase
    {
        public Guid DocumentDefinitionId { get; set; }

        // public DocumentDefinition? DocumentDefinition { get; set; }
        public Guid DocumentFormatId { get; set; }

        public DocumentFormat? DocumentFormat { get; set; }

    }
}