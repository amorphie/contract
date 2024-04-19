namespace amorphie.contract.application.Contract.Dto
{
    public class UserSignedContractInputDto
    {
        public List<Guid> DocumentInstanceIds { get; set; } = new();
        public Guid ContractInstanceId { get; set; }
        public string ContractCode { get; set; }

        public void SetHeaderParameters(string userReference)
        {
            _userReference = userReference;
        }

        private string _userReference;

        public string GetUserReference()
        {
            ArgumentException.ThrowIfNullOrEmpty(_userReference);

            return _userReference;
        }
    }
}
