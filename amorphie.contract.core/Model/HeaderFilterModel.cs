using amorphie.contract.core.Enum;

namespace amorphie.contract.core.Model
{

    public class HeaderFilterModel
    {
        public EBankEntity EBankEntity { get; set; }

        public string LangCode { get; set; }

        public string ClientCode { get; set; }

        public string UserReference { get; set; }

    }
}