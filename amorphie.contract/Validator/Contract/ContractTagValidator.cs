using FluentValidation;
using amorphie.contract.core.Entity.Contract;

public sealed class ContractTagValidator : AbstractValidator<ContractTag>
    {
        public ContractTagValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }