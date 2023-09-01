using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentTypeValidator : AbstractValidator<DocumentType>
{
    public DocumentTypeValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}