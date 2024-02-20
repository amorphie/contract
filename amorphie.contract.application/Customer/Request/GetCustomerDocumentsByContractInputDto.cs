using System;
using amorphie.contract.core.Enum;

namespace amorphie.contract.application.Customer.Request
{
    public class GetCustomerDocumentsByContractInputDto
    {
        public string? Code { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        private string? _lang;
        private EBankEntity _bankEntity;

        private string _userReference;

        public void SetHeaderParameters(string langCode, EBankEntity bankEntity, string userReference)
        {
            if (String.IsNullOrEmpty(userReference))
                throw new ArgumentNullException("User_Reference cannot be null");

            _lang = langCode;
            _bankEntity = bankEntity;
            _userReference = userReference;
        }

        public string GetLanguageCode()
        {
            return _lang;
        }

        public EBankEntity GetBankEntityCode()
        {
            return _bankEntity;
        }

        public string GetUserReference()
        {
            return _userReference;
        }
    }
}

