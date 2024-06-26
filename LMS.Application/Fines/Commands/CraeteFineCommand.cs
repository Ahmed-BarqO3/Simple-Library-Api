using LMS.Application.Interface;
using LMS.Application.Response;
using LMS.Core.Models;
using Mapster;
using Mediator;

namespace LMS.Application.Fines.Commands;

public record CreateFineCommand(
    int FineId,
    int UserId,
    int BorrowingRecordId,
    byte? NumberOfLateDays,
    decimal FineAmount,
    bool PaymentStatus,
    BorrowingRecordResponse BorrowingRecord,
    UserResponse User) : IRequest<FinesResponse>;

public class CreateFineCommandHandler(IUnitOfWork context) : IRequestHandler<CreateFineCommand, FinesResponse>
{
    public async ValueTask<FinesResponse> Handle(CreateFineCommand request, CancellationToken cancellationToken)
    {
        var fine = new Fine
        {
            FineId = request.FineId,
            UserId = request.UserId,
            BorrowingRecordId = request.BorrowingRecordId,
            NumberOfLateDays = request.NumberOfLateDays,
            FineAmount = request.FineAmount,
            PaymentStatus = request.PaymentStatus
        };

        await context.Fines.AddAsync(fine);
         context.Save();

         return fine.Adapt<FinesResponse>();
    }
}

