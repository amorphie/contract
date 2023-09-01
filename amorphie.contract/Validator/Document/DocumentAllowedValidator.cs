using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentAllowedValidator : AbstractValidator<DocumentAllowed>
{
    public DocumentAllowedValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}