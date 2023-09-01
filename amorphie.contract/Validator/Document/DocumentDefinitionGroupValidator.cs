using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentDefinitionGroupValidator : AbstractValidator<DocumentDefinitionGroup>
{
    public DocumentDefinitionGroupValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}