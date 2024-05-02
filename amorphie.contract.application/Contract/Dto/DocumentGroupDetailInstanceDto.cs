using amorphie.contract.application.Contract.Dto;

namespace amorphie.contract.application
{

    public class DocumentGroupDetailInstanceDto : BaseDto
    {
        public List<DocumentInstanceDto> DocumentInstances { get; set; } = new List<DocumentInstanceDto>();

        public string Status { get; set; } = default!;
    }
}