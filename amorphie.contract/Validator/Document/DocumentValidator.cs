using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentValidator : AbstractValidator<Document>
{
    public DocumentValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}