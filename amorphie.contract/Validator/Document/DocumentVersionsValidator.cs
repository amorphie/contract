using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentVersionsValidator : AbstractValidator<DocumentVersions>
{
    public DocumentVersionsValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}