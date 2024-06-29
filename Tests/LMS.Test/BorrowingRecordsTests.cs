using FluentAssertions;
using FluentValidation;
using LMS.Application.BorrowingRecords.Commands;
using LMS.Application.BorrowingRecords.Commands.Handlers;
using LMS.Application.BorrowingRecords.Commands.Validation;
using LMS.Application.BorrowingRecords.Query;
using LMS.Application.BorrowingRecords.Query.Handlers;
using LMS.Application.Common;
using LMS.Application.Interface;
using LMS.Application.Response;
using LMS.Core.Models;
using Mapster;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace LMS.Test;

public class BorrowingRecordsTests
{
    private readonly IUnitOfWork context = Substitute.For<IUnitOfWork>();

    [Fact]
    public async Task CreateBorrowingCommand_ShouldReturnResponse_WhenBorrowingAdded()
    {
        // Arrange
        var command = new CreateBorrowingRecordCommand()
        {
            CopyId = 1, UserId = 1, DueDate = DateOnly.FromDateTime(DateTime.Now).AddDays(3),
        };

        var record = new BorrowingRecord()
        {
            CopyId = 1,
            UserId = 1,
            DueDate = DateOnly.FromDateTime(DateTime.Now).AddDays(3),
            BorrowingDate = DateOnly.FromDateTime(DateTime.Now)
        };

        await context.BorrowingRecords.AddAsync(command.Adapt<BorrowingRecord>());

        var handler = new CreateBorrowingRecordCommandHandler(context);
        // Act

        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(record.Adapt<BorrowingRecordResponse>());
    }

    [Theory]
    [InlineData(0, 0, "2022-12-12")]
    [InlineData(1, 1, "2022-12-12")]
    [InlineData(-1, -1, "2022-12-12")]
    public void CreateBorrowingRecorCommandValidator_ShouldThrowValidationException_WhenDataIsInvalid(int copyId,
        int userId, DateTime dueDate)
    {
        // Arrange
        var command = new CreateBorrowingRecordCommand()
        {
            CopyId = copyId, UserId = userId, DueDate = DateOnly.FromDateTime(dueDate),
        };

        var validator = new CraeteBorrowingValidator();

        // Act
        Action act = () => validator.ValidateAndThrow(command);

        // Assert
        act.Should().Throw<ValidationException>()
            .And.Message.Should().Contain("Due Date");
    }

    [Fact]
    public async Task
        UpdateBorrowingRecordCommandHandler_ShouldReturnBorrowingRecordResponse_WhenBorrowingRecordUpdated()
    {
        // Arrange
        var command = new UpdateBorrowingRecordCommand
        {
            BorrowingRecordId = 1, CopyId = 1, UserId = 1, DueDate = DateOnly.FromDateTime(DateTime.Now).AddDays(3)
        };

        var record = new BorrowingRecord
        {
            BorrowingRecordId = 1, CopyId = 1, UserId = 1, DueDate = DateOnly.FromDateTime(DateTime.Now).AddDays(3)
        };

        var handler = new UpdateBorrowingRecordCommandHandler(context);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(record.Adapt<BorrowingRecordResponse>());
    }

    [Theory]
    [InlineData(0, 0, "2022-12-12")]
    [InlineData(1, 1, "2022-12-12")]
    [InlineData(-1, -1, "2022-12-12")]
    public void UpdateBorrowingRecordCommandValidator_ShouldThrowValidationException_WhenDataIsInvalid(int copyId,
        int userId, DateTime dueDate)
    {
        // Arrange
        var command = new UpdateBorrowingRecordCommand()
        {
            BorrowingRecordId = 1, CopyId = copyId, UserId = userId, DueDate = DateOnly.FromDateTime(dueDate),
        };

        var validator = new UpdateBorrowingValidator();

        // Act
        Action act = () => validator.ValidateAndThrow(command);

        // Assert
        act.Should().Throw<ValidationException>()
            .And.Message.Should().Contain("Due Date");
    }

    [Fact]
    public async Task GetBorrowingRecordByIdQueryHandler_ShouldReturnBorrowingRecordResponse_WhenBorrowingRecordFound()
    {
        // Arrange
        var query = new GetBorrowingRecordByIdQuery(1);

        var record = new BorrowingRecord
        {
            BorrowingRecordId = 1, CopyId = 1, UserId = 1, DueDate = DateOnly.FromDateTime(DateTime.Now).AddDays(3)
        };

        context.BorrowingRecords.GetByIdAsync(1).Returns(record);

        var handler = new GetBorrowingRecordByIdQueryHandler(context);
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        // Assert
        result.Should().BeEquivalentTo(record.Adapt<BorrowingRecordResponse>());
    }

    [Fact]
    public async Task GetBorrowingRecordByIdQueryHandler_ShouldReturnNULL_WhenBorrowingRecordNotFound()
    {
        // Arrange
        var query = new GetBorrowingRecordByIdQuery(1);
        context.BorrowingRecords.GetByIdAsync(1).ReturnsNull();
        var handler = new GetBorrowingRecordByIdQueryHandler(context);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        // Assert
        result.Should().BeNull();
    }

    [Theory]
    [InlineData(5, 0)]
    [InlineData(0, 1)]
    [InlineData(5, 1)]
    [InlineData(5, -1)]
    [InlineData(-5, 1)]
    [InlineData(50, 1)]
    [InlineData(500, 1)]
    public async Task GetBorrowingRecordsQueryHandler_ShouldReturnBorrowingRecordResponse_WhenBorrowingRecordsFound(
        int size, int page)
    {
        // Arrange
        var filter = new PaginationFilter(size, page);
        var query = new GetBorrowingRecordsQuery(filter);

        var records = new List<BorrowingRecord>
        {
            new BorrowingRecord
            {
                BorrowingRecordId = 1,
                CopyId = 1,
                UserId = 1,
                DueDate = DateOnly.FromDateTime(DateTime.Now).AddDays(3)
            },
            new BorrowingRecord
            {
                BorrowingRecordId = 2,
                CopyId = 2,
                UserId = 2,
                DueDate = DateOnly.FromDateTime(DateTime.Now).AddDays(3)
            }
        };

        context.BorrowingRecords.GetAllAsync(CancellationToken.None, filter.pageSize, filter.pageNumber)
            .Returns(records);

        var handler = new GetBorrowingRecordsQueryHandler(context);
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        // Assert
        result.Should().BeEquivalentTo(records.Adapt<List<BorrowingRecordResponse>>());
    }
}
