using amorphie.core.Base;

namespace amorphie.contract.application
{
    public class DocumentDefinitionDto : BaseDto
    {
        public List<MultilanguageText> MultilanguageText { get; set; }
        public DocumentOperationsDto DocumentOperations { get; set; }
    }


}