
using amorphie.core.Module.minimal_api;
using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Document.DocumentTypes;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Document.DocumentTypes;

public class DocumentOnlineSingModule
    : AudiAdminModule<DocumentOnlineSing, DocumentOnlineSing, ProjectDbContext>
{
    public DocumentOnlineSingModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "Required" };
    public override string? UrlFragment => base.UrlFragment +"document-online-sing";




}

