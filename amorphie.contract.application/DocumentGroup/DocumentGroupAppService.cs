using amorphie.contract.core.CustomException;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Entity.Document.DocumentGroups;
using amorphie.contract.core.Model.History;
using amorphie.contract.infrastructure.Contexts;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using VaultSharp.V1.SecretsEngines.Identity;

namespace amorphie.contract.application
{
    public interface IDocumentGroupAppService
    {
        Task<DocumentGroup> CreateDocumentGroup(DocumentGroupInputDto inputDto, Guid id);
        Task<DocumentGroup> UpdateDocumentGroup(DocumentGroupInputDto inputDto, Guid id);
    }

    public class DocumentGroupAppService : IDocumentGroupAppService
    {
        private readonly ProjectDbContext _dbContext;

        public DocumentGroupAppService(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DocumentGroup> CreateDocumentGroup(DocumentGroupInputDto inputDto, Guid id)
        {
            var documentGroup = new DocumentGroup
            {
                Id = id,
                Code = inputDto.Code,
                Titles = inputDto.Titles
            };

            _dbContext.DocumentGroup.Add(documentGroup);

            CreateDocumentGroupDetail(inputDto.Documents, id);

            _dbContext.SaveChanges();

            return documentGroup;
        }

        public async Task<DocumentGroup> UpdateDocumentGroup(DocumentGroupInputDto inputDto, Guid id)
        {
            var documentGroup = _dbContext.DocumentGroup.AsSplitQuery().FirstOrDefault(x => x.Id == id);

            if (documentGroup == null)
            {
                throw new EntityNotFoundException("Document Group", id.ToString());
            }

            SetContractDefinitionHistory(documentGroup, id);
            documentGroup.Code = inputDto.Code;

            UpdateDocumentGroupDetail(documentGroup.DocumentGroupDetails.ToList(), inputDto.Documents, id);

            _dbContext.SaveChanges();

            return documentGroup;
        }

        private void CreateDocumentGroupDetail(List<DocumentGroupDocumentInputDto> list, Guid groupId)
        {
            var documentGroupDetail = list.Select(x => new DocumentGroupDetail
            {
                DocumentDefinitionId = _dbContext.DocumentDefinition.Where(y => y.Semver == x.MinVersiyon && y.Code == x.Code).Select(y => y.Id).FirstOrDefault(),
                DocumentGroupId = groupId,
            });

            _dbContext.DocumentGroupDetail.AddRange(documentGroupDetail);
        }

        private void UpdateDocumentGroupDetail(List<DocumentGroupDetail> entityList, List<DocumentGroupDocumentInputDto> list, Guid groupId)
        {
            List<DocumentGroupDocumentInputDto> addList = new List<DocumentGroupDocumentInputDto>();
            foreach (var detailObject in list)
            {
                var existItem = entityList.Any(x => x.DocumentDefinition.Code == detailObject.Code && x.DocumentDefinition.Semver == detailObject.MinVersiyon);

                if (!existItem)
                {
                    addList.Add(detailObject);
                }
            }

            CreateDocumentGroupDetail(addList, groupId);

            var removeDetails = entityList
                .Where(x => !list.Select(z => z.Code).Contains(x.DocumentDefinition.Code)
            &&
                !(list.Where(z => z.Code == x.DocumentDefinition.Code).Select(z => z.MinVersiyon).FirstOrDefault() == x.DocumentDefinition.Semver));

            _dbContext.DocumentGroupDetail.RemoveRange(entityList.Except(removeDetails).ToList());
        }

        private void SetContractDefinitionHistory(DocumentGroup existingDocumentGroup, Guid groupId)
        {
            var documentGroupHistoryModel = ObjectMapperApp.Mapper.Map<DocumentGroupHistoryModel>(existingDocumentGroup);
            var documentGroupHistory = new DocumentGroupHistory
            {
                DocumentGroupHistoryModel = documentGroupHistoryModel,
                DocumentGroupId = groupId
            };

            _dbContext.DocumentGroupHistory.Add(documentGroupHistory);
        }
    }
}

