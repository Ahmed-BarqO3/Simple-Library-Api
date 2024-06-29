using FluentValidation;
using FluentValidation.Validators;

namespace LMS.Application.BorrowingRecords.Commands.Validation;

public class UpdateBorrowingValidator : AbstractValidator<UpdateBorrowingRecordCommand>
{
    public UpdateBorrowingValidator()
    {
        RuleFor(x => x.DueDate).GreaterThan(DateOnly.FromDateTime(DateTime.Now));
    }
}
