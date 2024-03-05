using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentInstanceEntityPropertyValidator : AbstractValidator<DocumentInstanceEntityProperty>
{
    public DocumentInstanceEntityPropertyValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}