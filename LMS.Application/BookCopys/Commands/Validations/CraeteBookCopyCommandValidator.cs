using FluentValidation;

namespace LMS.Application.BookCopys.Commands.Validations;

public class CraeteBookCopyCommandValidator : AbstractValidator<CreateBookCopyCommand>
{
    public CraeteBookCopyCommandValidator()
    {
        RuleFor(i => i.BookId).NotEmpty().Must(x => x > 0);
    }
}
