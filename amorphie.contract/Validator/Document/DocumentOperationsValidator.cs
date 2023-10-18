using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentOperationsValidator : AbstractValidator<DocumentOperations>
{
    public DocumentOperationsValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}