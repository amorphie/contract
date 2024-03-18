using FluentValidation;
using amorphie.contract.core.Entity.Document;

namespace amorphie.contract;
public sealed class DocumentInstanceNoteValidator : AbstractValidator<DocumentInstanceNote>
{
    public DocumentInstanceNoteValidator()
    {
        // RuleFor(x => x.Name).NotNull();
        // RuleFor(x => x.Name).MinimumLength(10);
    }
}