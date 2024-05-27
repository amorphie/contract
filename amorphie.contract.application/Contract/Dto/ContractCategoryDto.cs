namespace amorphie.contract.application.Contract.Dto
{
    public class ContractCategoryDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public List<ContractCategoryDetailDto> ContractCategoryDetails { get; set; } = new List<ContractCategoryDetailDto>();
        public Dictionary<string, string> Titles { get; set; }
    }
}