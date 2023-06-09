using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentTemplate", Schema = "Doc")]

    public class DocumentTemplate : EntityBase
    {
        //Render edilecekler
        public string Name { get; set; }
        public string URL { get; set; }
    }
}