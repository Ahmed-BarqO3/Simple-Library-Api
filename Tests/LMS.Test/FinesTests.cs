using FluentAssertions;
using LMS.Application.Common;
using LMS.Application.Fines.Query;
using LMS.Application.Interface;
using LMS.Application.Response;
using LMS.Core.Models;
using Mapster;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace LMS.Test;

public class FinesTests
{
    readonly IUnitOfWork _context = Substitute.For<IUnitOfWork>();

    [Fact]
    public async Task GetFineByIdQueryHandler_ShouldReturnFineResponse_WhenFineFound()
    {
        // Arrange
        var fine = new Fine
        {
            FineId = 1,
            FineAmount = 7,
            PaymentStatus = true,
            BorrowingRecordId = 1,
            NumberOfLateDays = 0,
            UserId = 3
        };

        _context.Fines.GetByIdAsync(1).Returns(fine);
        var handler = new GetFineByIdQueryHandler(_context);

        // Act
        var result = await handler.Handle(new GetFineByIdQuery(1), CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(fine.Adapt<FinesResponse>());
    }
    
    [Fact]
    public async Task GetFineByIdQueryHandler_ShouldReturnNull_WhenFineNotFound()
    {
        // Arrange
        _context.Fines.GetByIdAsync(1).ReturnsNull();
        var handler = new GetFineByIdQueryHandler(_context);

        // Act
        var result = await handler.Handle(new GetFineByIdQuery(1), CancellationToken.None);
        
        // Assert
        result.Should().BeNull();
    }
    
    [Theory]
    [InlineData(10,1)]
    [InlineData(5,2)]
    [InlineData(-1,2)]
    [InlineData(5,-1)]
    [InlineData(0,0)]
    [InlineData(500,1)]
    public async Task GetFinesQueryHandler_ShouldReturnListOfFinesResponse(int size,int page)
    {
        // Arrange
        var filter = new PaginationFilter(size, page);
        
        var fines = new List<Fine>
        {
            new Fine
            {
                FineId = 1,
                FineAmount = 7,
                PaymentStatus = true,
                BorrowingRecordId = 1,
                NumberOfLateDays = 0,
                UserId = 3
            },
            new Fine
            {
                FineId = 2,
                FineAmount = 5,
                PaymentStatus = false,
                BorrowingRecordId = 2,
                NumberOfLateDays = 2,
                UserId = 4
            }
        };

        _context.Fines.GetAllAsync(CancellationToken.None, filter.pageSize,filter.pageNumber,Arg.Any<string[]>()).ReturnsForAnyArgs(fines);
        var handler = new GetFinesQueryHandler(_context);

        // Act
        var result = await handler.Handle(new GetFinesQuery(filter), CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(fines.Adapt<List<FinesResponse>>());
    }
}
