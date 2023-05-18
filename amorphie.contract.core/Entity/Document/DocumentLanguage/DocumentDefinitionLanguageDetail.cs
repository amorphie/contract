using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Entity.Common;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document.DocumentLanguage
{
    [Table("DocumentDefinitionLanguageDetail", Schema = "Definition")]
    public class DocumentDefinitionLanguageDetail : EntityBase
    {
        public Guid LanguageId { get; set; }

        public  Language Language { get; set; }
        public Guid DocumentDefinitionId { get; set; }

        public DocumentDefinition DocumentDefinition { get; set; }
    }
}