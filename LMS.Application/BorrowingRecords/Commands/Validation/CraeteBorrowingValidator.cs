using System.Data;
using FluentValidation;

namespace LMS.Application.BorrowingRecords.Commands.Validation;

public class CraeteBorrowingValidator : AbstractValidator<CreateBorrowingRecordCommand>
{
    public CraeteBorrowingValidator()
    {
        RuleFor(x => x.BorrowingDate).Equals(DateOnly.FromDateTime(DateTime.Now));
        RuleFor(x => x.DueDate).GreaterThan(DateOnly.FromDateTime(DateTime.Now));
    }
    
}
