using System;
using amorphie.core.Base;
using System.Text.Json.Serialization;

namespace amorphie.contract.application.Customer.Dto
{
    public class CustomerContractDocumentGroupDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string DocumentGroupStatus { get; set; } = AppConsts.NotValid;
        public ushort AtLeastRequiredDocument { get; set; }
        public bool Required { get; set; }
        public List<CustomerContractDocumentDto> CustomerContractGroupDocuments { get; set; }
        [JsonIgnore]
        public List<MultilanguageText> MultiLanguageText { get; set; }

        [JsonIgnore]
        public Dictionary<string, string> Titles { get; set; } = default!;

    }
}

