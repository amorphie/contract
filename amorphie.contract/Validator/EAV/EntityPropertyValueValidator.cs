using FluentValidation;
using amorphie.contract.core.Entity.EAV;

public sealed class EntityPropertyValueValidator : AbstractValidator<EntityPropertyValue>
{
    public EntityPropertyValueValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}