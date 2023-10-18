using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentTagsDetailValidator : AbstractValidator<DocumentTagsDetail>
{
    public DocumentTagsDetailValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}