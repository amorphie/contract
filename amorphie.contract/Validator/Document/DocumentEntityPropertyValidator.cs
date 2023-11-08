using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentEntityPropertyValidator : AbstractValidator<DocumentEntityProperty>
{
    public DocumentEntityPropertyValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}