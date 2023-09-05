using FluentValidation;
using amorphie.contract.core.Entity.Document.DocumentTypes;

public sealed class DocumentRenderValidator : AbstractValidator<DocumentRender>
    {
        public DocumentRenderValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }