﻿using System;

namespace amorphie.contract.application.Customer.Dto
{
    public class CustomerContractDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string ContractStatus { get; set; } = AppConsts.NotValid;
        public List<CustomerContractDocumentDto> CustomerContractDocuments { get; set; }
        public List<CustomerContractDocumentGroupDto> CustomerContractDocumentGroups { get; set; }
    }
}

