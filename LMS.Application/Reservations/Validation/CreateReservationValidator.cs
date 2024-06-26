using FluentValidation;
using LMS.Application.Reservations.Commands;

namespace LMS.Application.Reservations.Validation;

public class CreateReservationValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.CopyId).NotEmpty();
        RuleFor(x => x.ReservationDate).NotEmpty().Equal(DateOnly.FromDateTime(DateTime.Now));
    }
}
