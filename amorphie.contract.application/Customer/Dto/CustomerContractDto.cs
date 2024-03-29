﻿using System;
using amorphie.core.Base;
using System.Text.Json.Serialization;

namespace amorphie.contract.application.Customer.Dto
{
    public class CustomerContractDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string ContractStatus { get; set; } = AppConsts.NotValid;
        [JsonIgnore]
        public List<MultilanguageText> MultiLanguageText { get; set; }
        public List<CustomerContractDocumentDto> CustomerContractDocuments { get; set; }
        public List<CustomerContractDocumentGroupDto> CustomerContractDocumentGroups { get; set; }
    }
}

