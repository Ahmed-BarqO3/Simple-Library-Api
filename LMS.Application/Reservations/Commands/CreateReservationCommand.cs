using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.Reservations.Commands;

public record CreateReservationCommand(int UserId, int CopyId, DateOnly ReservationDate) : IRequest<ReservationResponse>;


public class CreateReservationCommandHandler(IUnitOfWork context) : IRequestHandler<CreateReservationCommand, ReservationResponse>
{


    public async ValueTask<ReservationResponse> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = new Core.Models.Reservation
        {
            UserId = request.UserId,
            CopyId = request.CopyId,
            ReservationDate = request.ReservationDate
        };

        await context.Reservations.AddAsync(reservation);
        context.Save();
        
        return reservation.Adapt<ReservationResponse>();
    }
}
