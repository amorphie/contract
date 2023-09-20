using FluentValidation;
using amorphie.contract.core.Entity.Document;
using amorphie.contract.core.Entity.Document.DocumentGroups;

public sealed class DocumentGroupDetailValidator : AbstractValidator<DocumentGroupDetail>
{
    public DocumentGroupDetailValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}