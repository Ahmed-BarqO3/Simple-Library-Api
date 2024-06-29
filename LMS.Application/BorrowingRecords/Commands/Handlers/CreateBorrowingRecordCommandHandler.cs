using LMS.Application.Common;
using LMS.Application.Interface;
using LMS.Application.Response;
using LMS.Core.Models;
using Mapster;
using Mediator;

namespace LMS.Application.BorrowingRecords.Commands.Handlers;

public class CreateBorrowingRecordCommandHandler(IUnitOfWork context) : IRequestHandler<CreateBorrowingRecordCommand,BorrowingRecordResponse>
{
    public async ValueTask<BorrowingRecordResponse> Handle(CreateBorrowingRecordCommand request,
        CancellationToken cancellationToken)
    {
        var record = request.Adapt<BorrowingRecord>();
        context.BorrowingRecords.AddAsync(record);

        context.Save();
        return request.Adapt<BorrowingRecordResponse>();
    }
}
