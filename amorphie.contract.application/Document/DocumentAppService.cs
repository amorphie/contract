using amorphie.contract.data.Contexts;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;


namespace amorphie.contract.application
{

    public class DocumentAppService : IDocumentAppService
    {
        private readonly ProjectDbContext _dbContext;
        public DocumentAppService(ProjectDbContext projectDbContext)
        {
            _dbContext = projectDbContext;
        }

        public async Task<List<RootDocumentDto>> GetAllDocumentFullTextSearch(GetAllDocumentInputDto input, CancellationToken cancellationToken)
        {

            var response = new List<RootDocumentDto>();

            var query = _dbContext!.Document
                                    .Skip(input.Page * input.PageSize)
                                    .Take(input.PageSize);


            if (!string.IsNullOrEmpty(input.Keyword))
            {
                query = query.Where(x => x.Status.ToString() == input.Keyword);
            }

            var securityQuestions = await query.ToListAsync(cancellationToken);

            if (securityQuestions.Any())
            {

                response = securityQuestions.Select(x =>
                new RootDocumentDto
                {
                    Id = x.Id,
                    DocumentDefinitionId = x.DocumentDefinitionId.ToString(),
                    StatuCode = x.Status.ToString(),
                    CreatedAt = x.CreatedAt,
                    DocumentDefinition = new DocumentDefinitionDto
                    {
                        Code = x.DocumentDefinition.Code,
                        MultilanguageText = ObjectMapper.Mapper.Map<List<MultilanguageText>>(x.DocumentDefinition.DocumentDefinitionLanguageDetails),
                        DocumentOperations = new DocumentOperationsDto
                        {
                            DocumentManuelControl = x.DocumentDefinition.DocumentOperations!.DocumentManuelControl,
                            DocumentOperationsTagsDetail = ObjectMapper.Mapper.Map<List<TagDto>>(x.DocumentDefinition.DocumentOperations.DocumentOperationsTagsDetail!.Select(t => t.Tags))

                        }
                    },
                    DocumentContent = ObjectMapper.Mapper.Map<DocumentContentDto>(x.DocumentContent)
                }).ToList();

            }

            return response;
        }


    }
    public interface IDocumentAppService
    {
        public Task<List<RootDocumentDto>> GetAllDocumentFullTextSearch(GetAllDocumentInputDto input, CancellationToken cancellationToken);
    }
}
