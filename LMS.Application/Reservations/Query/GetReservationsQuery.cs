using LMS.Application.Common;
using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.Reservations.Query;

public record GetReservationsQuery(PaginationFilter Filter) : IRequest<List<ReservationResponse>>;


class  GetReservationsQueryHandler(IUnitOfWork context) : IRequestHandler<GetReservationsQuery, List<ReservationResponse>>
{
    public async ValueTask<List<ReservationResponse>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
    {
        var result = await  context.Reservations.GetAllAsync(cancellationToken, request.Filter.pageSize,
            request.Filter.pageNumber, new[] { "User", "BookCopy" });
        
        return result.Adapt<List<ReservationResponse>>();
    }
}
