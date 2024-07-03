namespace amorphie.contract.application.Contract.Dto
{
    public class ContractDecisionTagOutputDto
    {
        public string DecisionTableId { get; set; }
        public Dictionary<string, string> Tags { get; set; }
    }
}