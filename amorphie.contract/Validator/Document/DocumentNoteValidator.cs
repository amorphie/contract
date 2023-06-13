using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentNoteValidator : AbstractValidator<DocumentNote>
    {
        public DocumentNoteValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }