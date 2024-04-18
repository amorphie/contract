using amorphie.contract.core.Model;

namespace amorphie.contract.application.Contract.Dto
{
    public class UserSignedContractInputDto
    {
        public List<Guid> DocumentInstanceIds { get; set; } = new();
        public Guid ContractInstanceId { get; set; }
        public string ContractCode { get; set; }

        public void SetHeaderParameters(HeaderFilterModel headerFilterModel)
        {
            UserReference = headerFilterModel.UserReference;
        }

        public string UserReference { get; private set; }
    }
}
