
using amorphie.contract.core.Extensions;
using amorphie.contract.core.Response;
using amorphie.contract.infrastructure.Contexts;
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

        public async Task<GenericResult<IEnumerable<DocumentDefinitionDto>>> GetAllDocumentDefinition(GetAllDocumentDefinitionInputDto input, CancellationToken cancellationToken)
        {
            var list = await _dbContext.DocumentDefinition.OrderBy(x => x.Code).Skip(input.Page * input.PageSize)
                .Take(input.PageSize).AsNoTracking().ToListAsync(cancellationToken);

            var responseDtos = list.Select(x => ObjectMapperApp.Mapper.Map<DocumentDefinitionDto>(x, opt => opt.Items[Lang.LangCode] = input.LangCode)).ToList();

            return GenericResult<IEnumerable<DocumentDefinitionDto>>.Success(responseDtos);

        }
    }

    public interface IDocumentDefinitionAppService
    {
        Task<GenericResult<IEnumerable<DocumentDefinitionDto>>> GetAllDocumentDefinition(GetAllDocumentDefinitionInputDto input, CancellationToken cancellationToken);
    }
}