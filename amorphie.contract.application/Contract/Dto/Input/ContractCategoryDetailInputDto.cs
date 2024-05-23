using System;
namespace amorphie.contract.application.Contract.Dto.Input
{
	public class ContractCategoryDetailInputDto
	{
		public Guid? ContractCategoryId { get; set; }
		public Guid? ContractDefinitionId { get; set; }
		public List<Guid> AddIdList { get; set; }
	}
}

