using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentFormIO", Schema = "Doc")]

    public class DocumentFormIO : EntityBase
    {
        //Render edilecekler
        public string Name { get; set; }
        public string Data { get; set; }
    }
}