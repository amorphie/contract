using System;
using amorphie.contract.core.Enum;

namespace amorphie.contract.application.Customer.Request
{
    public class GetCustomerDocumentsByContractInputDto
    {
        public string? Code { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Reference { get; set; }
        private string? _lang;
        private EBankEntity _bankEntity;

        public void SetHeaderParameters(string langCode, EBankEntity bankEntity)
        {
            _lang = langCode;
            _bankEntity = bankEntity;
        }

        public string GetLanguageCode()
        {
            return _lang;
        }

        public EBankEntity GetBankEntityCode()
        {
            return _bankEntity;
        }
    }
}

