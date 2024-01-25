using amorphie.core.Base;

namespace amorphie.contract.application
{

    public class DocumentGroupDto : BaseDto
    {
        public List<DocumentDefinitionDto> DocumentDefinitions { get; set; } = default!;

        public string Status { get; set; } = default!;

        public List<MultilanguageText> MultilanguageText { get; set; } = default!;

    }
}