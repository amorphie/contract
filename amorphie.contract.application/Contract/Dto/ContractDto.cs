using amorphie.contract.core.Model.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amorphie.contract.application.Contract.Dto
{
    public class ContractDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public List<DocumentDto> Document { get; set; }
        public List<DocumentGroupDto> DocumentGroups { get; set; }
    }
}
