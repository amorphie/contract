using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentDefinitionLanguageDetailValidator : AbstractValidator<DocumentDefinitionLanguageDetail>
{
    public DocumentDefinitionLanguageDetailValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}