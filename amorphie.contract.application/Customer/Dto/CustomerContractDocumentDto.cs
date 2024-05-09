using System;
using amorphie.contract.application.Contract.Dto;
using amorphie.core.Base;
using System.Text.Json.Serialization;

namespace amorphie.contract.application.Customer.Dto
{
    public class CustomerContractDocumentDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string DocumentStatus { get; set; } = AppConsts.NotValid;
        public bool Required { get; set; }
        public bool Render { get; set; }
        public string Version { get; set; }
        public string MinioUrl { get; set; }
        public DateTime ApprovalDate { get; set; }
        public OnlineSignDto OnlineSign { get; set; }

        [JsonIgnore]
        public Dictionary<string, string> Titles { get; set; } = default!;
        [JsonIgnore]
        public bool IsDeleted { get; set; }

    }
}

