using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentAllowedType", Schema = "Doc")]

    public class DocumentAllowedType : EntityBase
    {
        public string Name { get; set; }
    }
}