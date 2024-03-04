using amorphie.contract.core.Entity.Document;
using FluentValidation;

public sealed class DocumentTsizlValidator : AbstractValidator<DocumentTsizl>
    {
        public DocumentTsizlValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }