using FluentValidation;

namespace LMS.Application.Books.Commands.Validation;
public class UpdateBookCommandValidator: AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(x => x.Title).MinimumLength(3);
        RuleFor(x => x.Isbn).NotEmpty().WithMessage("ISBN is required");
        RuleFor(x => x.PublicationDate).NotEmpty().WithMessage("Publication Date is required");
    }
}
