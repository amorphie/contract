using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentFormIODetailValidator : AbstractValidator<DocumentFormIODetail>
    {
        public DocumentFormIODetailValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }