using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;


namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentType", Schema = "Doc")]
    public class DocumentType : EntityBase
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
    }
}