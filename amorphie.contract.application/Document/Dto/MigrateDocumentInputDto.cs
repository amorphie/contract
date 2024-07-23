using amorphie.contract.core.Model.Documents;

namespace amorphie.contract.application
{
    public class MigrateDocumentInputDto
    {
        public DocumentContentDto DocumentContent { get; set; }
        public DocumentContentDto? DocumentContentOriginal { get; set; }

        public List<DocumentMigrationContractModel> DocumentMigrationContracts { get; set; } = default!;

        public required Guid CustomerId { get; set; }
        public required Guid DocumentDefinitionId { get; set; }
        public required string DocumentCode { get; set; }
        public string DocumentVersion { get; set; }
        public List<NoteDto>? Notes { get; set; }
        public List<MetadataDto> InstanceMetadata { get; set; } = default!;

        public required string UserReference { get; set; }

    }



}