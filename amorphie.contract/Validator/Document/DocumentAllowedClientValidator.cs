using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentAllowedClientValidator : AbstractValidator<DocumentAllowedClient>
{
    public DocumentAllowedClientValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}