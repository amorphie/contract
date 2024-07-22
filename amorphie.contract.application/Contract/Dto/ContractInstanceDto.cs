﻿using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Enum;

namespace amorphie.contract.application.Contract.Dto
{
    public class ContractInstanceDto
    {
        public ContractInstanceDto()
        {
            Status = ApprovalStatus.InProgress.ToString();
        }

        [Required]
        public string Code { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }

        [Required]
        public Guid ContractInstanceId { get; set; }
        public List<DocumentInstanceDto> DocumentList { get; set; } = new List<DocumentInstanceDto>();
        public List<DocumentInstanceResultDto> DocumentApprovedList { get; set; } = new List<DocumentInstanceResultDto>();
        public List<DocumentGroupInstanceDto> DocumentGroupList { get; set; } = new List<DocumentGroupInstanceDto>();

    }
}
