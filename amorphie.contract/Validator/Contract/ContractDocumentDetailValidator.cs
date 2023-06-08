using FluentValidation;
using amorphie.contract.core.Entity.Contract;

public sealed class ContractDocumentDetailValidator : AbstractValidator<ContractDocumentDetail>
    {
        public ContractDocumentDetailValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }