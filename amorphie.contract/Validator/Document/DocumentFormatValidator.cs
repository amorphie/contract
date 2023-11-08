using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentFormatValidator : AbstractValidator<DocumentFormat>
{
    public DocumentFormatValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}