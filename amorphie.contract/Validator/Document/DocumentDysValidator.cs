using amorphie.contract.core.Entity.Document;
using FluentValidation;

public sealed class DocumentDysValidator : AbstractValidator<DocumentDys>
    {
        public DocumentDysValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }