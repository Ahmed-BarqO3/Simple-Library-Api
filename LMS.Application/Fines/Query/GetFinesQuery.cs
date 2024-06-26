using LMS.Application.Common;
using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.Fines.Query;

public record GetFinesQuery(PaginationFilter Filter) : IRequest<List<FinesResponse>>;


public class GetFinesQueryHandler(IUnitOfWork context) : IRequestHandler<GetFinesQuery, List<FinesResponse>>
{
    public async ValueTask<List<FinesResponse>> Handle(GetFinesQuery request, CancellationToken cancellationToken)
    {
        var fine = await context.Fines.GetAllAsync(cancellationToken, request.Filter.pageSize,
            request.Filter.pageNumber, new[] { "User", "BorrowingRecord" });

        return fine.Adapt<List<FinesResponse>>();
    }
}
