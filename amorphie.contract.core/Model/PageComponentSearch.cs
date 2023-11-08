using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Base;

namespace amorphie.contract.core.Model
{
    public class PageComponentSearch : DtoSearchBase
    {

    }
    public class ComponentSearch
    {
        public required string Keyword { get; set; }
    }
}