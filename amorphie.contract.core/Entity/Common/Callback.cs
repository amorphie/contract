using System.ComponentModel.DataAnnotations.Schema;
using amorphie.contract.core.Entity.Document;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

namespace amorphie.contract.core.Entity.Common
{
    [Table("Callback", Schema = "Common")]

    public class Callback : EntityBase
    {
        public string Code { get; set; }
        public string Url { get; set; }
        public string ApiKey { get; set; }
    }
}