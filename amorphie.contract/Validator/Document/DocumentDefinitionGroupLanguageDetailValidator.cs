using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentDefinitionGroupLanguageDetailValidator : AbstractValidator<DocumentDefinitionGroupLanguageDetail>
{
    public DocumentDefinitionGroupLanguageDetailValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}