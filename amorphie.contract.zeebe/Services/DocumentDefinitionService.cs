using amorphie.contract.core.Entity.Common;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Document.DocumentTypes;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.zeebe.Model.DocumentDefinitionDataModel;
using Newtonsoft.Json;
using amorphie.contract.core.Enum;
using amorphie.contract.zeebe.Helper;
using amorphie.contract.core.Model.Documents;
using amorphie.contract.core.Model;
using amorphie.contract.application.Document.Dto.Zebee;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Model.Proxy;
using amorphie.contract.core.CustomException;
using amorphie.contract.application;

namespace amorphie.contract.zeebe.Services
{
    public interface IDocumentDefinitionService
    {
        Task<DocumentDefinition> CreateDocumentDefinition(DocumentDefinitionInputDto inputDto, Guid id);
        Task<DocumentDefinition> UpdateDocumentDefinition(DocumentDefinitionInputDto inputDto, Guid id);
    }
    public class DocumentDefinitionService : IDocumentDefinitionService
    {
        private readonly IDocumentDefinitionAppService _documentDefinitionAppService;

        public DocumentDefinitionService(DocumentDefinitionAppService documentDefinitionAppService)
        {
            _documentDefinitionAppService = documentDefinitionAppService;
        }

        public async Task<DocumentDefinition> CreateDocumentDefinition(DocumentDefinitionInputDto inputDto, Guid id)
        {
            var documentDefinition = await _documentDefinitionAppService.CreateDocumentDefinition(inputDto, id);

            return documentDefinition;
        }

        public async Task<DocumentDefinition> UpdateDocumentDefinition(DocumentDefinitionInputDto inputDto, Guid id)
        {
            var documentDefinition = await _documentDefinitionAppService.UpdateDocumentDefinition(inputDto, id);

            return documentDefinition;
        }
    }
}