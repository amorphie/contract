using FluentValidation;
using amorphie.contract.core.Entity.Document.DocumentTypes;

public sealed class DocumentOnlineSingValidator : AbstractValidator<DocumentOnlineSing>
{
    public DocumentOnlineSingValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}