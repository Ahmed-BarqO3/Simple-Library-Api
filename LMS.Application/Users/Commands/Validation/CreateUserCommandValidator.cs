using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using LMS.Core.Models;

namespace LMS.Application.Users.Commands.Validation
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters");

            RuleFor(x => x.ContactInformation)
                .NotEmpty().WithMessage("Contact information is required")
                .MaximumLength(50).WithMessage("Contact information must not exceed 50 characters");
        }

    }
}
