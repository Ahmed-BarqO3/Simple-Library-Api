using FluentValidation;

namespace LMS.Application.Books.Commands.Validation;
public class CreateBookCommandVlidator: AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandVlidator()
    {
        RuleFor(x => x.Title).MinimumLength(3);
        RuleFor(x => x.Isbn).NotEmpty().WithMessage("ISBN is required");
        RuleFor(x => x.PublicationDate).NotEmpty().WithMessage("Publication Date is required");
    }
}
