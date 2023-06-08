using FluentValidation;
using amorphie.contract.core.Entity.Contract;

public sealed class ContractDefinitionValidator : AbstractValidator<ContractDefinition>
    {
        public ContractDefinitionValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }