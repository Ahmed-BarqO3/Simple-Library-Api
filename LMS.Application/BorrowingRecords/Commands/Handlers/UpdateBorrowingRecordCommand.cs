using LMS.Application.Interface;
using LMS.Application.Response;
using LMS.Core.Models;
using Mapster;
using Mediator;

namespace LMS.Application.BorrowingRecords.Commands.Handlers;

public class UpdateBorrowingRecordCommandHandler(IUnitOfWork context) : IRequestHandler<UpdateBorrowingRecordCommand,BorrowingRecordResponse>
{
   
    public async ValueTask<BorrowingRecordResponse> Handle(UpdateBorrowingRecordCommand request, CancellationToken cancellationToken)
    {
        var record = await context.BorrowingRecords.GetByIdAsync(request.BorrowingRecordId);
        var result = context.BorrowingRecords.Update(record.Adapt<BorrowingRecord>());

        context.Save();

        return result.Adapt<BorrowingRecordResponse>();
    }
}
