using FluentValidation;

namespace LMS.Application.BookCopys.Commands.Validations;

public class CreateBookCopyCommandValidator : AbstractValidator<CreateBookCopyCommand>
{
    public CreateBookCopyCommandValidator()
    {
        RuleFor(i => i.BookId).NotNull().Must(x => x > 0);
    }
}
