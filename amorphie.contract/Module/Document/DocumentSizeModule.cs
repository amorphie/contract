
using amorphie.core.Module.minimal_api;
using amorphie.contract.data.Contexts;
using amorphie.core.Repository;
using FluentValidation;
using amorphie.core.Base;
using amorphie.contract.core.Entity.Document;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace amorphie.contract;

    public class DocumentSizeModule
        : BaseContractModule<DocumentSize, DocumentSize, DocumentSizeValidator>
    {
        public DocumentSizeModule(WebApplication app) : base(app)
        {
        }

        // public override string[]? PropertyCheckList => new string[] {"Code","Contact"};

        public override string? UrlFragment => "document-size";


        // public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
        // {
        //     base.AddRoutes(routeGroupBuilder);

        //     routeGroupBuilder.MapPost("custom", ([FromServices]IBBTRepository<DocumentSize, ProjectDbContext> repository) => { 
            
        //         return repository.GetAll();
        //     });
            
        // }
        // // You can override any method in basemodule
        // protected override ValueTask<IResult> GetAll(
        //     [FromServices] IBBTRepository<DocumentSize, ProjectDbContext> repository, 
        //     [FromQuery, Range(0, 100)] int page, 
        //     [FromQuery, Range(5, 100)] int pageSize)
        // {
        //     return base.GetAll(repository, page, pageSize);
        // } 
       
    }

