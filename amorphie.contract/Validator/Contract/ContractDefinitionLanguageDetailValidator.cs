using amorphie.contract.core;
using FluentValidation;

public sealed class ContractDefinitionLanguageDetailValidator : AbstractValidator<ContractDefinitionLanguageDetail>
{
    public ContractDefinitionLanguageDetailValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}