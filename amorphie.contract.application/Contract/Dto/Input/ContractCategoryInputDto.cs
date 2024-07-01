using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.application.Common;

namespace amorphie.contract.application.Contract.Dto.Input
{
    public class ContractCategoryInputDto
    {
        public string Code { get; set; }
        public List<ContractCategoryDetailDto> ContractCategoryDetails { get; set; } = new List<ContractCategoryDetailDto>();
        public Dictionary<string, string> Titles
        {
            get
            {
                return TitleInput.ToDictionary(x => x.Key, x => x.Value);
            }
        }
        public List<TitleInputDto> TitleInput { get; set; }
    }
}