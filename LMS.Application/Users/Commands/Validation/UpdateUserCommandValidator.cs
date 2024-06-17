using FluentValidation;

namespace LMS.Application.Users.Commands.Validation
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ContactInformation).EmailAddress();
        }
    }
}
