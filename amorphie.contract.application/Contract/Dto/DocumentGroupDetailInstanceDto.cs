using amorphie.contract.application.Contract.Dto;
using amorphie.core.Base;

namespace amorphie.contract.application
{

    public class DocumentGroupDetailInstanceDto : BaseDto
    {
        public List<DocumentInstanceDto> DocumentInstances { get; set; } = default!;

        public string Status { get; set; } = default!;
    }
}