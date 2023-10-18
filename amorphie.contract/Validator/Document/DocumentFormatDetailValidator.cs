using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentFormatDetailValidator : AbstractValidator<DocumentFormatDetail>
{
    public DocumentFormatDetailValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}