using amorphie.contract.core.Enum;

namespace amorphie.contract.core.Model
{

    public class HeaderFilterModel
    {
        public HeaderFilterModel(string businessLine, string langCode, string clientCode, string userReference, long? customerNo)
        {

            EBankEntity = GetBankEntity(businessLine);
            
            if (!string.IsNullOrEmpty(langCode))
            {
                int commaIndex = langCode.IndexOf(',');
                this.LangCode = commaIndex != -1 ? langCode.Substring(0, commaIndex) : langCode;
            }
            ClientCode = clientCode;
            UserReference = userReference;
            CustomerNo = customerNo;
        }
        public EBankEntity EBankEntity { get; private set; }

        public string LangCode { get; set; }

        public string ClientCode { get; set; }

        public string UserReference { get; set; }

        public long? CustomerNo { get; set; }
        public EBankEntity GetBankEntity(string businessLine)
        {
            return businessLine switch
            {
                "X" => EBankEntity.on,
                "B" => EBankEntity.burgan,
                _ => EBankEntity.on
                // _ => throw new NotImplementedException($"{nameof(EBankEntity)} is not yet implemented.")
            };
        }
    }
}