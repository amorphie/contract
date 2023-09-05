using FluentValidation;
using amorphie.contract.core.Entity.Document.DocumentTypes;

public sealed class DocumentUploadValidator : AbstractValidator<DocumentUpload>
{
    public DocumentUploadValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}