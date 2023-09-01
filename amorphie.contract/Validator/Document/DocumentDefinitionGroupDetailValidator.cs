using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentDefinitionGroupDetailValidator : AbstractValidator<DocumentDefinitionGroupDetail>
{
    public DocumentDefinitionGroupDetailValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}