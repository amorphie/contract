using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentOptimizeTypeValidator : AbstractValidator<DocumentOptimizeType>
{
    public DocumentOptimizeTypeValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}