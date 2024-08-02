using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.application.Contract.Dto.Zeebe;
using Microsoft.AspNetCore.Http;

namespace amorphie.contract.application
{
    public class DocumentUploadInstanceInputDto : BaseHeader
    {
        public DocumentUploadInstanceInputDto()
        {
            DocumentInstanceId = Guid.NewGuid();
        }
        public Guid DocumentInstanceId { get; set; }
        public Guid? ContractInstanceId { get; set; }
        public string? ContractCode { get; set; }
        [Required]
        public required string DocumentCode { get; set; }
        [Required]
        public required string DocumentVersion { get; set; }
        public long? CustomerNo { get; private set; }
        public List<MetadataDto>? InstanceMetadata { get; set; }
        public List<NoteDto>? Notes { get; set; }
        public DocumentContentDto DocumentContent { get; set; }
        public ContractWithoutHeaderDto? ContractWithoutHeader { get; set; }

    }
    public record DocumentUploadInstanceOutputDto
    {
        public Guid DocumentInstanceId { get; init; }
    }
}