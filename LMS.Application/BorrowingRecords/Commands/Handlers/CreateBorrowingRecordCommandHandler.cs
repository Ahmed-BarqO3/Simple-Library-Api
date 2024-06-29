using LMS.Application.Common;
using LMS.Application.Interface;
using LMS.Application.Response;
using LMS.Core.Models;
using Mapster;
using Mediator;

namespace LMS.Application.BorrowingRecords.Commands.Handlers;

public class CreateBorrowingRecordCommandHandler(IUnitOfWork context) : IRequestHandler<CreateBorrowingRecordCommand,BorrowingRecordResponse>
{
    public async ValueTask<BorrowingRecordResponse> Handle(CreateBorrowingRecordCommand request, CancellationToken cancellationToken)
    {
        var record = request.Adapt<BorrowingRecord>();
        var reslut =  context.BorrowingRecords.AddAsync(record);
        if (reslut.IsCompletedSuccessfully)
        {
            context.Save();
            return reslut.Adapt<BorrowingRecordResponse>();
        }
        return null;

    }
}
