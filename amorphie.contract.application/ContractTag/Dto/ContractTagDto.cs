namespace amorphie.contract.application
{
    public class ContractTagDto : BaseDto
    {
        public Guid ContractDefinitionId { get; set; }
        public Guid TagId { get; set; }
        public TagDto Tags { get; set; } = default!;

    }
}
