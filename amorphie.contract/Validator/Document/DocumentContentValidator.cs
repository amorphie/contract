using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentContentValidator : AbstractValidator<DocumentContent>
{
    public DocumentContentValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}