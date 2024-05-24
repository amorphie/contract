using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.application.Contract.Dto
{
    public class ContractCategoryResponseDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public Dictionary<string, string> CategoryContracts { get; set; }
    }
}