using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentAllowedClientDetailValidator : AbstractValidator<DocumentAllowedClientDetail>
    {
        public DocumentAllowedClientDetailValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }