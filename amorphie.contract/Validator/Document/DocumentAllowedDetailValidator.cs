using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentAllowedDetailValidator : AbstractValidator<DocumentAllowedDetail>
{
    public DocumentAllowedDetailValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}