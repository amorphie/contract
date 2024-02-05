using amorphie.contract.data.Contexts;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using amorphie.contract.core.Entity.Contract;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using amorphie.contract.application.Contract.Dto;
using amorphie.contract.application;
using amorphie.core.Extension;
using amorphie.contract.core.Enum;
using amorphie.contract.core.Model;
using amorphie.contract.application.Contract;
using amorphie.contract.application.Contract.Request;
using amorphie.contract.core.Enum;

namespace amorphie.contract;

public class ContractDefinitionModule
    : BaseBBTContractRoute<ContractDefinitionDto, ContractDefinition, ProjectDbContext>
{
    public ContractDefinitionModule(WebApplication app) : base(app)
    {

    }

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapGet("GetExistContract", GetExist);
    }
    protected async override ValueTask<IResult> GetAllMethod([FromServices] ProjectDbContext context, [FromServices] IMapper mapper, [FromQuery, Range(0, 100)] int page, [FromQuery, Range(5, 100)] int pageSize, HttpContext httpContext, CancellationToken token, [FromQuery] string? sortColumn, [FromQuery] SortDirectionEnum? sortDirection)
    {
        try
        {
            var headerModels = httpContext.Items[AppHeaderConsts.HeaderFilterModel] as HeaderFilterModel;


            var query = context!.ContractDefinition!.Skip(page)
                .Take(pageSize).AsNoTracking().AsSplitQuery();


            var list = await query.ToListAsync(token);

            var respose = ObjectMapperApp.Mapper.Map<List<ContractDefinitionDto>>(list);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // list.ForEach(x => x.ContractDocumentGroupDetailLists.ForEach(a => a.DocumentGroup.DocumentDefinitionList.ForEach(b =>
            // b.Name = b.MultilanguageText.Any(c => c.Language == language) ? b.MultilanguageText.FirstOrDefault(c => c.Language == language).Label
            // : b.MultilanguageText.First().Label)
            // ));
            // list.ForEach(x => x.ContractDocumentGroupDetailLists.ForEach(b =>
            // b.DocumentGroup.Name = b.DocumentGroup.MultilanguageText.Any(c => c.Language == language) ? b.DocumentGroup.MultilanguageText.FirstOrDefault(c => c.Language == language).Label
            // : b.DocumentGroup.MultilanguageText.First().Label)
            // );
            // list.ForEach(x => x.ContractDocumentDetailList?.ForEach(b =>
            // b.DocumentDefinition.Name = b.DocumentDefinition.MultilanguageText.Any(c => c.Language == language) ? b.DocumentDefinition.MultilanguageText.FirstOrDefault(c => c.Language == language).Label
            // : b.DocumentDefinition.MultilanguageText.First().Label)
            // );
            stopwatch.Stop();
            TimeSpan elapsed = stopwatch.Elapsed;
            return Results.Ok(respose);

        }
        catch (Exception ex)
        {
            Results.Problem(ex.Message);
        }
        return Results.NoContent();
    }

    async ValueTask<IResult> GetExist([FromServices] IContractAppService contractAppService, CancellationToken token, [FromQuery] string? code, [FromQuery] EBankEntity? eBankEntity)
    {
        var req = new ContractGetExistInputDto()
        {
            Code = code,
            EBankEntity = (EBankEntity)eBankEntity
        };
        var response = await contractAppService.GetExist(req, token);

        return Results.Ok(response);
    }
    public override string[]? PropertyCheckList => new string[] { "Code" };

    public override string? UrlFragment => "contract-definition";
}

