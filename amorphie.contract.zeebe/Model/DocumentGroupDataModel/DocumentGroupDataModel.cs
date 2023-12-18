using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.zeebe.Model.DocumentGroupDataModel
{
    public class DocumentGroupDataModel
    {
        public string code { get; set; }
        public List<string> document { get; set; }
        public List<Title> titles { get; set; }
    }


    public class Title
    {
        public string language { get; set; }
        public string title { get; set; }
    }
}