using FluentValidation;
using amorphie.contract.core.Entity.EAV;

public sealed class EntityPropertyValidator : AbstractValidator<EntityProperty>
{
    public EntityPropertyValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}