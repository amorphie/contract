using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentSizeValidator : AbstractValidator<DocumentSize>
    {
        public DocumentSizeValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }