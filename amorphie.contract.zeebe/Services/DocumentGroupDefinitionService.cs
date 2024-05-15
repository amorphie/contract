using amorphie.contract.core.Entity.Common;
using amorphie.contract.infrastructure.Contexts;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using amorphie.contract.zeebe.Model.DocumentGroupDataModel;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.application;
using amorphie.contract.core.Model.History;
using amorphie.contract.core.Response;

namespace amorphie.contract.zeebe.Services
{
    public interface IDocumentGroupDefinitionService
    {
        Task<GenericResult<DocumentGroup>> CreateDocumentGroup(DocumentGroupInputDto inputDto, Guid id);
        Task<GenericResult<DocumentGroup>> UpdateDocumentGroup(DocumentGroupInputDto inputDto, Guid id);
    }
    public class DocumentGroupDefinitionService : IDocumentGroupDefinitionService
    {
        private readonly IDocumentGroupAppService _documentGroupAppService;

        public DocumentGroupDefinitionService(IDocumentGroupAppService documentGroupAppService)
        {
            _documentGroupAppService = documentGroupAppService;
        }

        public async Task<DocumentGroup> CreateDocumentGroup(DocumentGroupInputDto inputDto, Guid id)
        {
            return await _documentGroupAppService.CreateDocumentGroup(inputDto, id);
        }

        public async Task<DocumentGroup> UpdateDocumentGroup(DocumentGroupInputDto inputDto, Guid id)
        {
            return await _documentGroupAppService.UpdateDocumentGroup(inputDto, id);
        }
    }
}