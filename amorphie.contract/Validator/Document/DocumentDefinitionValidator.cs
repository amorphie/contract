using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentDefinitionValidator : AbstractValidator<DocumentDefinition>
    {
        public DocumentDefinitionValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }