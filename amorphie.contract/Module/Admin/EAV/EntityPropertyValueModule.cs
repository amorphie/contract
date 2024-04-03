using amorphie.contract.infrastructure.Contexts;
using amorphie.contract.core.Entity.EAV;
using amorphie.contract.Module.Base;

namespace amorphie.contract.Module.Admin.EAV;

public class EntityPropertyValueModule :
 AudiAdminModule<EntityPropertyValue, EntityPropertyValue, ProjectDbContext>
{
    public EntityPropertyValueModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "Data" };
    public override string? UrlFragment => base.UrlFragment + "entity-property-value";

}

