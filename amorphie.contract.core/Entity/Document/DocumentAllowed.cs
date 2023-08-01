using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentAllowed", Schema = "Doc")]

    public class DocumentAllowed : EntityBase
    {
        //Render edilecekler
        public string Name { get; set; }
        public DocumentAllowedType? DocumentAllowedType { get; set; }//client,
    }
}