using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentFormatTypeValidator : AbstractValidator<DocumentFormatType>
    {
        public DocumentFormatTypeValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }