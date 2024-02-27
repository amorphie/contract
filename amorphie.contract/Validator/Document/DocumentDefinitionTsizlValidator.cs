using amorphie.contract.core.Entity.Document;
using FluentValidation;

public sealed class DocumentDefinitionTsizlValidator : AbstractValidator<DocumentDefinitionTsizl>
    {
        public DocumentDefinitionTsizlValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }