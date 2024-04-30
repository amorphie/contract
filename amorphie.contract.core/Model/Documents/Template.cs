using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.core.Model.Documents
{
    public class Template
    {
        public string Code { get; set; }
        public string LanguageCode { get; set; } = default!;
        public string Version { get; set; } = default!;
    }
}