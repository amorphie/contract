using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentGroupLanguageDetailValidator : AbstractValidator<DocumentGroupLanguageDetail>
    {
        public DocumentGroupLanguageDetailValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }