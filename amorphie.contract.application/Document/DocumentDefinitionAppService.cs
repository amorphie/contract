using amorphie.contract.data.Contexts;
using Microsoft.EntityFrameworkCore;


namespace amorphie.contract.application
{

    public class DocumentDefinitionAppService : IDocumentDefinitionAppService
    {
        private readonly ProjectDbContext _dbContext;

        public DocumentDefinitionAppService(ProjectDbContext projectDbContext)
        {
            _dbContext = projectDbContext;
        }

        public async Task<IEnumerable<DocumentDefinitionDto>> GetAllDocumentDefinition(GetAllDocumentDefinitionInputDto input, CancellationToken cancellationToken)
        {

            var list = await _dbContext.DocumentDefinition.OrderBy(x => x.Code).Skip(input.Page * input.PageSize)
                .Take(input.PageSize).AsNoTracking().ToListAsync(cancellationToken);


            var responseDtos = list.Select(x => ObjectMapperApp.Mapper.Map<DocumentDefinitionDto>(x)).ToList();


            foreach (var documentDefinitionDto in responseDtos)
            {
                var selectedLanguageText = documentDefinitionDto?.MultilanguageText
                    .FirstOrDefault(t => t.Language == input.Lang.LangCode);

                if (selectedLanguageText != null)
                {
                    documentDefinitionDto.Name = selectedLanguageText.Label;
                }
                else if (documentDefinitionDto.MultilanguageText.Any())
                {
                    documentDefinitionDto.Name = documentDefinitionDto.MultilanguageText.First().Label;
                }
            }

            return responseDtos;

        }
    }

    public interface IDocumentDefinitionAppService
    {
        Task<IEnumerable<DocumentDefinitionDto>> GetAllDocumentDefinition(GetAllDocumentDefinitionInputDto input, CancellationToken cancellationToken);
    }
}