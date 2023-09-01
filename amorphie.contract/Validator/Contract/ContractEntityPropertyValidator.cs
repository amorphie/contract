using FluentValidation;
using amorphie.contract.core.Entity.Contract;

public sealed class ContractEntityPropertyValidator : AbstractValidator<ContractEntityProperty>
{
    public ContractEntityPropertyValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}