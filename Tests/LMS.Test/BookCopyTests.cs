using FluentAssertions;
using FluentValidation;
using LMS.Application.BookCopys.Commands;
using LMS.Application.BookCopys.Commands.Handlers;
using LMS.Application.BookCopys.Commands.Validations;
using LMS.Application.BookCopys.Query;
using LMS.Application.BookCopys.Query.Handlers;
using LMS.Application.Common;
using LMS.Application.Interface;
using LMS.Application.Response;
using LMS.Core.Models;
using Mapster;
using NSubstitute;


namespace LMS.Test;

public class BookCopyTests
{
    private readonly IUnitOfWork _context = Substitute.For<IUnitOfWork>();
    [Fact]
    public async Task CreateCommandCopyBookHandler_ShouldReturnBookCopyResponse_WhenBookCopyAdded()
    {
        // Arrange
        var command = new CreateBookCopyCommand()
        {
            BookId = 8
        };
        
        var handler = new CreateBookCopyCommandHandler(_context);
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
       Assert.Equal(result.BookId,command.BookId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public  void CreateCommandCopyBookValidator_ThrowValidationException_WhenBookCopyInvalid(int Id)
    {
        // Arrange
        var command = new CreateBookCopyCommand()
        {
            BookId = Id
        };
        var validator = new CreateBookCopyCommandValidator();
        
        // Act
        Action result = () => validator.ValidateAndThrow(command);
        
        // Assert
        result.Should().Throw<ValidationException>()
            .And.Message.Should().Contain("Book Id");
    }

    [Fact]
    public async Task DeleteCommandBookCopy_ShouldReturnBookCopyResponse_WhenBookCopyExist()
    {
        // Arrange
        var command = new DeleteBookCopyCommand(1);
        var copy = new BookCopy()
        {
            CopyId = 1,
            BookId = 1,
            AvailabilityStatus = true,
            Book = new()
            {
                BookId = 1,
                Title = "b",
                Isbn = "501378",
                PublicationDate = DateOnly.FromDateTime(new DateTime(2024, 6, 29)),
            }
        };
        _context.BookCopies.GetByIdAsync(1).Returns(copy);
        await _context.BookCopies.Delete(copy);
        var handler = new DeleteBookCopyCommandHandler(_context);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(copy.Adapt<BookCopyResponse>());
    }
    
    [Fact]
    public async Task DeleteCommandBookCopy_ShouldReturnBookCopyResponse_WhenBookCopyNotExist()
    {
        // Arrange
        var command = new DeleteBookCopyCommand(1);
        
        var handler = new DeleteBookCopyCommandHandler(_context);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReturnBookCopyResponse_WhenBookCopyFound()
    {
        // Arrange
        var query = new GetBookCopyQuery(1);
        var copy = new BookCopy()
        {
            CopyId = 1,
            BookId = 1,
            AvailabilityStatus = true,
        };
        _context.BookCopies.GetByIdAsync(1).Returns(copy);
        var handler = new GetBookCopyQueryHandler(_context);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(copy.Adapt<BookCopyResponse>());

    }
    
    [Fact]
    public async Task ShouldReturnBookCopyResponse_WhenBookCopyNotFound()
    {
        // Arrange
        var query = new GetBookCopyQuery(1);
     

        var handler = new GetBookCopyQueryHandler(_context);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().BeNull();

    }

    [Theory]
    [InlineData(1, 5)]
    [InlineData(2, 3)]
    [InlineData(3, 2)]
    [InlineData(0, 0)]
    [InlineData(-1, -1)]
    public async Task GetAllCopiesQueryHandler_ShouldReturnCopies(int number, int size)
    {
        // Arrange
        var filter = new PaginationFilter(size,number);
        var query = new GetAllCopiesQuery(filter);

        var copies = new List<BookCopy>()
        {
            new()
            {
                CopyId = 1,
                BookId = 1,
                AvailabilityStatus = true,
                Book = new Book {
                    BookId = 1,
                    Title = "b",
                    Isbn = "501378",
                    PublicationDate = DateOnly.FromDateTime(new DateTime(2024, 6, 29)) } },
            new()
            {
                CopyId = 2,
                BookId = 2,
                AvailabilityStatus = true,
                Book = new Book {
                    BookId = 2,
                    Title = "b",
                    Isbn = "501378",
                    PublicationDate = DateOnly.FromDateTime(new DateTime(2024, 6, 29)) }
            }
        };

        _context.BookCopies.GetAllAsync(CancellationToken.None,  filter.pageSize,  filter.pageNumber, Arg.Any<string[]>()).Returns(copies);
        var handler = new GetAllCopiesQueryHandler(_context);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(copies.Adapt<List<BookCopyResponse>>());
    }
}
