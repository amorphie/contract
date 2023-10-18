using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentOptimizeValidator : AbstractValidator<DocumentOptimize>
{
    public DocumentOptimizeValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}