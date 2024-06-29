using FluentAssertions;
using LMS.Application.InformationBooks;
using LMS.Application.Interface;
using LMS.Application.Response;
using LMS.Core.Models;
using Mapster;
using NSubstitute;

namespace LMS.Test;

public class InformationBooksTests
{
    readonly IUnitOfWork _context = Substitute.For<IUnitOfWork>();
    [Fact]
    public async Task GetInformationBooksQuryHandler_ShouldReturnInformationBooksResponse()
    {
        // Arrange
        var query = new GetInformationBooksQury();
        var informationBooks = new List<InformationBook>
            {
                new()
                {   BookId = 1,
                    Title = "Book1",
                    Isbn = "123456789",
                    PublicationDate = new DateOnly(2021, 10, 10),
                    AdditionalDetails = "Details",
                    NumberOfBooks = 10
                },
                new()
                {   BookId = 2,
                    Title = "Book2",
                    Isbn = "123456789",
                    PublicationDate = new DateOnly(2021, 10, 10),
                    AdditionalDetails = "Details",
                    NumberOfBooks = 10
                }
            };
        
        var handler = new GetInformationBooksQuryHandler(_context);
         _context.InformationBooks.GetByExecuteStoredProc(Arg.Any<FormattableString>())
            .Returns(informationBooks);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(informationBooks.Adapt<List<InformationBookResponse>>());
    }
}
