using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Common;

public class MultiLanguageModule
        : AudiAdminModule<MultiLanguage, MultiLanguage, ProjectDbContext>
{
    public MultiLanguageModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "Name" };
    public override string? UrlFragment => base.UrlFragment +  "common-multilanguage" ;
}

