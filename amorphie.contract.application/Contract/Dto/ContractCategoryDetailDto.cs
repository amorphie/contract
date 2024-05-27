namespace amorphie.contract.application.Contract.Dto
{
	public class ContractCategoryDetailDto
	{
		public Guid ContractDefinitionId { get; set; }
		public ContractDefinitionDto ContractDefinition { get; set; }
		public Guid ContractCategoryId { get; set; }
		public ContractCategoryDto ContractCategory { get; set; }
	}
}