using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Entity.Document
{
    [Table("DocumentOptimize", Schema = "Doc")]
    public class DocumentOptimize : EntityBase
    {//Document Defination a baglı olmalı
        public bool Size {get;set;}
        public string Transform {get;set;}//degişcek
    }
}