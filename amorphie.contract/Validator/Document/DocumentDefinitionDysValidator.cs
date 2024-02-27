using amorphie.contract.core.Entity.Document;
using FluentValidation;

public sealed class DocumentDefinitionDysValidator : AbstractValidator<DocumentDefinitionDys>
    {
        public DocumentDefinitionDysValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }