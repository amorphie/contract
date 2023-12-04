using System.Security.Cryptography.X509Certificates;

using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;

using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Contract;
using amorphie.contract.core.Mapping;
using AutoMapper;
using amorphie.contract.core.Model.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace amorphie.contract;

public class ContractDefinitionModule
    : BaseBBTContractRoute<ContractDefinition, ContractDefinition, ProjectDbContext>
{
    public ContractDefinitionModule(WebApplication app) : base(app)
    {

    }

    public override string[]? PropertyCheckList => new string[] { "Code" };

    public override string? UrlFragment => "contract-definition";


    protected override async ValueTask<IResult> GetAllMethod([FromServices] ProjectDbContext context, [FromServices] IMapper mapper,
           [FromQuery][Range(0, 100)] int page, [FromQuery][Range(5, 100)] int pageSize, HttpContext httpContext, CancellationToken token)
    {

        try
        {
            var language = httpContext.Request.Headers["Language"].ToString();
            if (string.IsNullOrEmpty(language))
            {
                language = "en-EN";
            }
            var query = context!.ContractDefinition!.Select(x => ObjectMapper.Mapper.Map<ContractDefinitionViewModel>(x)).Skip(page)
                .Take(pageSize).AsNoTracking().AsSplitQuery();
            var list = await query.ToListAsync(token);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            list.ForEach(x => x.ContractDocumentGroupDetailLists.ForEach(a => a.DocumentGroup.DocumentDefinitionList.ForEach(b =>
            b.Name = b.MultilanguageText.Any(c => c.Language == language) ? b.MultilanguageText.FirstOrDefault(c => c.Language == language).Label
            : b.MultilanguageText.First().Label)
            ));
            list.ForEach(x => x.ContractDocumentGroupDetailLists.ForEach(b =>
            b.DocumentGroup.Name = b.DocumentGroup.MultilanguageText.Any(c => c.Language == language) ? b.DocumentGroup.MultilanguageText.FirstOrDefault(c => c.Language == language).Label
            : b.DocumentGroup.MultilanguageText.First().Label)
            );
            list.ForEach(x => x.ContractDocumentDetailList.ForEach(b =>
            b.DocumentDefinition.Name = b.DocumentDefinition.MultilanguageText.Any(c => c.Language == language) ? b.DocumentDefinition.MultilanguageText.FirstOrDefault(c => c.Language == language).Label
            : b.DocumentDefinition.MultilanguageText.First().Label)
            );
            stopwatch.Stop();
            TimeSpan elapsed = stopwatch.Elapsed;
            return Results.Ok(list);

        }
        catch (Exception ex)
        {
            Results.Problem(ex.Message);
        }
        return Results.NoContent();

    }
}

