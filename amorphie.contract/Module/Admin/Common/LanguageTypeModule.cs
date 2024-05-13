using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.Common;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.Common;

public class LanguageTypeModule
        : BaseAdminModule<LanguageType, LanguageType, ProjectDbContext>
{
    public LanguageTypeModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "Code" };
    public override string? UrlFragment => base.UrlFragment + "common-languagetype";
}

