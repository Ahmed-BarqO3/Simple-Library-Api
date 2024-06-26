using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.Fines.Query;

public record GetFineByIdQuery(int FineId) : IRequest<FinesResponse>;

public class GetFineByIdQueryHandler(IUnitOfWork context) : IRequestHandler<GetFineByIdQuery, FinesResponse?>
{
    public async ValueTask<FinesResponse?> Handle(GetFineByIdQuery request, CancellationToken cancellationToken)
    {
        var fine = await context.Fines.GetByIdAsync(request.FineId);

        return fine.Adapt<FinesResponse?>();
    }
}
