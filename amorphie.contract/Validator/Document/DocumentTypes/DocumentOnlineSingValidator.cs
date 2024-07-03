using FluentValidation;
using amorphie.contract.core.Entity.Document.DocumentTypes;

public sealed class DocumentOnlineSignValidator : AbstractValidator<DocumentOnlineSign>
    {
        public DocumentOnlineSignValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }