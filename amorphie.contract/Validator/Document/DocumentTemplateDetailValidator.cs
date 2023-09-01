using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentTemplateDetailValidator : AbstractValidator<DocumentTemplateDetail>
{
    public DocumentTemplateDetailValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}