using LMS.Application.Common;
using LMS.Application.Common.Helpers;
using LMS.Application.Interface;
using LMS.Application.Reservations.Commands;
using LMS.Application.Reservations.Query;
using LMS.Application.Response;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controller;

[Route(ApiRoutes.Reservation.Get)]
[ApiController]
public class ReservationController(IMediator mediator,IUriService uri) : ControllerBase
{
    
    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommand command)
    {
        var result = await mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.ReservationId }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetReservations([FromQuery] PaginationQuery paginationQuery)
    {
        var filter = new PaginationFilter(paginationQuery.pageSize, paginationQuery.pageNumber);
        var query = new GetReservationsQuery(filter);

        var result = await mediator.Send(query);

        var response =
            PaginationHelper<ReservationResponse>.CreatePaginatedResponse(uri, ApiRoutes.Reservation.Get, filter,
                result);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetReservationbyIdQuery(id);
        var result = await mediator.Send(query);
        
        if(result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
