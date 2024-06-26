using LMS.Application.Common;
using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.Reservations.Query;

public record GetReservationbyIdQuery(int Id) : IRequest<ReservationResponse?>;


class  GetReservationbyIdQueryHandler(IUnitOfWork context) : IRequestHandler<GetReservationbyIdQuery, ReservationResponse?>
{
    public async ValueTask<ReservationResponse?> Handle(GetReservationbyIdQuery request, CancellationToken cancellationToken)
    {
        var result = await context.Reservations.GetByIdAsync(request.Id);
        
        return result.Adapt<ReservationResponse>();
    }
}
