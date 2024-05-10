using System.ComponentModel.DataAnnotations;

namespace amorphie.contract.application.Contract.Dto
{
    public class UserSignedContractInputDto
    {
        public List<Guid> DocumentInstanceIds { get; set; } = new();

        [Required]
        public required Guid ContractInstanceId { get; set; }

        [Required]
        public required string ContractCode { get; set; }

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
