using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.InformationBooks;

public record GetInformationBooksQury(): IRequest<List<InformationBook>>;


class GetInformationBooksQuryHandler(IUnitOfWork context) : IRequestHandler<GetInformationBooksQury, List<InformationBook>>
{
    public async ValueTask<List<InformationBook>> Handle(GetInformationBooksQury request, CancellationToken cancellationToken)
    {
        var books = await context.InformationBooks.GetByExecuteStoredProc($"SP_GetAllBooks");
        
        return books.Adapt<List<InformationBook>>();
    }
}
