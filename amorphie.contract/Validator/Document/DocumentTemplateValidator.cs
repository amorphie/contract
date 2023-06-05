using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentTemplateValidator : AbstractValidator<DocumentTemplate>
    {
        public DocumentTemplateValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }