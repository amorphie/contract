using FluentValidation;
using amorphie.contract.core.Entity.EAV;

public sealed class EntityPropertyTypeValidator : AbstractValidator<EntityPropertyType>
    {
        public EntityPropertyTypeValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }