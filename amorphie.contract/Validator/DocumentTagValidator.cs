using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentTagValidator : AbstractValidator<DocumentTag>
    {
        public DocumentTagValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }