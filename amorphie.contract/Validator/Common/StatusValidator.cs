using FluentValidation;
using amorphie.contract.core.Entity.Common;
public sealed class StatusValidator : AbstractValidator<Status>
{
    public StatusValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}