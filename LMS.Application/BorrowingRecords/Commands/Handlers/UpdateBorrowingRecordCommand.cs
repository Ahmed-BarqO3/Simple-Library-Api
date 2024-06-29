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
       
        context.BorrowingRecords.Update(request.Adapt<BorrowingRecord>());
        context.Save();

        return request.Adapt<BorrowingRecordResponse>();
    }
}
