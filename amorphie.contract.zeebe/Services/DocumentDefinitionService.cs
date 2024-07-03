using amorphie.contract.application;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Response;

namespace amorphie.contract.zeebe.Services
{
    public interface IDocumentDefinitionService
    {
        Task<GenericResult<DocumentDefinition>> CreateDocumentDefinition(DocumentDefinitionInputDto inputDto, Guid id);
        Task<GenericResult<DocumentDefinition>> UpdateDocumentDefinition(DocumentDefinitionInputDto inputDto, Guid id);
    }
    public class DocumentDefinitionService : IDocumentDefinitionService
    {
        private readonly IDocumentDefinitionAppService _documentDefinitionAppService;

        public DocumentDefinitionService(IDocumentDefinitionAppService documentDefinitionAppService)
        {
            _documentDefinitionAppService = documentDefinitionAppService;
        }

        public async Task<GenericResult<DocumentDefinition>> CreateDocumentDefinition(DocumentDefinitionInputDto inputDto, Guid id)
        {
            return await _documentDefinitionAppService.CreateDocumentDefinition(inputDto, id);
        }

        public async Task<GenericResult<DocumentDefinition>> UpdateDocumentDefinition(DocumentDefinitionInputDto inputDto, Guid id)
        {
            return await _documentDefinitionAppService.UpdateDocumentDefinition(inputDto, id);
        }
    }
}