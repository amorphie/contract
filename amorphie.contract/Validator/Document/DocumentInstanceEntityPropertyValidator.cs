using FluentValidation;
using amorphie.contract.core.Entity.Document;

namespace amorphie.contract;
public sealed class DocumentInstanceEntityPropertyValidator : AbstractValidator<DocumentInstanceEntityProperty>
    {
        public DocumentInstanceEntityPropertyValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }