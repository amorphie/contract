using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentTemplate", Schema = "Definition")]

    public class DocumentTemplate : EntityBase
    {
        public string Name { get; set; }
    }
}