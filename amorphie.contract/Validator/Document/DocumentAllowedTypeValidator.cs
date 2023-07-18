using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentAllowedTypeValidator : AbstractValidator<DocumentAllowedType>
    {
        public DocumentAllowedTypeValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }