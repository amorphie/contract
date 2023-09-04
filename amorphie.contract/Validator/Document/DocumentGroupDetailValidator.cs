using FluentValidation;
using amorphie.contract.core.Entity.Document;

public sealed class DocumentGroupDetailValidator : AbstractValidator<DocumentGroupDetail>
    {
        public DocumentGroupDetailValidator()
        {
            // RuleFor(x => x.Name).NotNull();
            // RuleFor(x => x.Name).MinimumLength(10);
        }
    }