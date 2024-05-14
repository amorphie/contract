using System.ComponentModel.DataAnnotations;

namespace amorphie.contract.application.Contract.Dto
{
    public class IsUserApprovedContractInputDto
    {

        public IsUserApprovedContractInputDto(string contractCode, string userReference)
        {
            ContractCode = contractCode;
            _userReference = userReference;
        }

        [Required]
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
