using FluentValidation;
using amorphie.contract.core.Entity.Contract;

public sealed class ContractValidationValidator : AbstractValidator<ContractValidation>
    {
        public ContractValidationValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }