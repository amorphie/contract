using FluentValidation;
using amorphie.contract.core.Entity.Contract;

public sealed class ContractDocumentGroupDetailValidator : AbstractValidator<ContractDocumentGroupDetail>
    {
        public ContractDocumentGroupDetailValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }