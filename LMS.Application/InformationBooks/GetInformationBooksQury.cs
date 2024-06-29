using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.InformationBooks;

public record GetInformationBooksQury(): IRequest<List<InformationBookResponse>>;

public class GetInformationBooksQuryHandler(IUnitOfWork context) : IRequestHandler<GetInformationBooksQury, List<InformationBookResponse>>
{
    public async ValueTask<List<InformationBookResponse>> Handle(GetInformationBooksQury request, CancellationToken cancellationToken)
    {
        var books = await context.InformationBooks.GetByExecuteStoredProc($"SP_GetAllBooks");
        
        return books.Adapt<List<InformationBookResponse>>();
    }
}
