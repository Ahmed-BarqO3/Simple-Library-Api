using FluentAssertions;
using FluentValidation;
using LMS.Application.Books.Commands;
using LMS.Application.Books.Commands.HandlerCommands;
using LMS.Application.Books.Commands.Validation;
using LMS.Application.Books.Querys;
using LMS.Application.Books.Querys.HandlersQuerys;
using LMS.Application.Common;
using LMS.Application.Interface;
using LMS.Application.Response;
using LMS.Core.Models;
using Mapster;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace LMS.Test;

public class BookTests
{
    readonly IUnitOfWork _context = Substitute.For<IUnitOfWork>();
    
    [Fact]
    public async Task CreateBook_ShouldReturnBookResponse_WhenBookAdded()
    {
        // Arrange
        var command = new CreateBookCommand()
        {
            Title = "book", Isbn = "7895231",PublicationDate = DateOnly.FromDateTime(DateTime.Now)
        };
        
        var handler = new CreateBookCommandHandler(_context);
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
   
        // Assert
        result.Should().BeEquivalentTo(command.Adapt<BookResponse>());
    }
    
    [Fact]
    public async Task UpdateBook_ShouldReturnBookResponse_WhenBookUpdated()
    {
        // Arrange
        var command = new UpdateBookCommand()
        {
            BookId = 1, Title = "book", Isbn = "7895231",PublicationDate = DateOnly.FromDateTime(DateTime.Now)
        };
        
        var handler = new UpdateBookCommandHandler(_context);
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
   
        // Assert
        result.Should().BeEquivalentTo(command.Adapt<BookResponse>());
    }
    
    [Fact]
    public void CreateCommandBookValidator_ShouldThrowValidationException_WhenCommandInvalid()
    {
        // Arrange
        var command = new CreateBookCommand()
        {
            Title = "", Isbn = "",PublicationDate = DateOnly.FromDateTime(DateTime.MaxValue)
        };
        
        var validator = new CreateBookCommandVlidator();
        // Act
        Action act = () => validator.ValidateAndThrow(command);
   
        // Assert
        act.Should().Throw<ValidationException>()
            .And.Message.Should().Contain("Title")
            .And.Contain("ISBN")
            .And.Contain("Publication Date");
    }
    
    [Fact]
    public void UpdateCommandBookValidator_ShouldThrowValidationException_WhenCommandInvalid()
    {
        // Arrange
        var command = new UpdateBookCommand()
        {
            Title = "", Isbn = "",PublicationDate = DateOnly.FromDateTime(DateTime.MaxValue)
        };
        
        var validator = new UpdateBookCommandValidator();
        // Act
        Action act = () => validator.ValidateAndThrow(command);
   
        // Assert
        act.Should().Throw<ValidationException>()
            .And.Message.Should().Contain("Title")
            .And.Contain("ISBN")
            .And.Contain("Publication Date");
    }
    
    [Fact]
    public async Task GetBookbyIdQueryHandler_ShouldReturnBookResponse_WhenBookFound()
    {
        // Arrange
        var book = new Book()
        {
            BookId = 1, Title = "book", Isbn = "7895231",PublicationDate = DateOnly.FromDateTime(DateTime.Now)
        };

         _context.Books.GetByIdAsync(1).Returns(book);
        
        var handler = new GetBookByIdHandel(_context);
        var query = new GetBookByIdQuery(1);
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
   
        // Assert
        result.Should().BeEquivalentTo(book.Adapt<BookResponse>());
    }
    
    [Fact]
    public async Task GetBookbyIdQueryHandler_ShouldReturnNull_WhenBookNotFound()
    {
        // Arrange
        _context.Books.GetByIdAsync(Arg.Any<int>()).ReturnsNull();
        
        var handler = new GetBookByIdHandel(_context);
        var query = new GetBookByIdQuery(Arg.Any<int>());
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
   
        // Assert
        result.Should().BeNull();
    }

    [Theory]
    [InlineData(1, 5)]
    [InlineData(1, 100)]
    [InlineData(1, 200)]
    [InlineData(-1, -1)]
    [InlineData(0, 0)]
    public async Task GetAllBookQueryHandel_ShouldReturnBookResponse_WhenBooksFound(int PageNumber, int PageSize)
    {
        // Arrange
        var books = new List<Book>
        {   new() { BookId = 1, Title = "book", Isbn = "7895231", PublicationDate = DateOnly.FromDateTime(DateTime.Now) },
            new() { BookId = 2, Title = "book2", Isbn = "7895232", PublicationDate = DateOnly.FromDateTime(DateTime.Now) },
            new() { BookId = 3, Title = "book2", Isbn = "7895232", PublicationDate = DateOnly.FromDateTime(DateTime.Now) }
        };
        
        var pagination = new PaginationQuery { pageNumber = PageNumber, pageSize = PageSize };

        var filter = new PaginationFilter(pagination.pageSize, pagination.pageNumber);

        _context.Books.GetAllAsync(CancellationToken.None, filter.pageSize, filter.pageNumber).Returns(books);

        var handler = new GetAllBooksQueryHandel(_context);
        var query = new GetAllBooksQuery(filter);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(books.Adapt<List<BookResponse>>());
    }
}
