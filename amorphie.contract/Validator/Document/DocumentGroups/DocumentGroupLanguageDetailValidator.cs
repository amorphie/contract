using FluentValidation;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Document.DocumentGroups;

public sealed class DocumentGroupLanguageDetailValidator : AbstractValidator<DocumentGroupLanguageDetail>
{
    public DocumentGroupLanguageDetailValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}