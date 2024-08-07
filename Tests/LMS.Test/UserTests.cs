using FluentAssertions;
using FluentValidation;
using LMS.Application.Common;
using LMS.Application.Interface;
using LMS.Application.Response;
using LMS.Application.Users.Commands;
using LMS.Application.Users.Commands.Handlers;
using LMS.Application.Users.Commands.Validation;
using LMS.Application.Users.Query;
using LMS.Application.Users.Query.Handlers;
using LMS.Core.Models;
using Mapster;
using NSubstitute;

using NSubstitute.ReturnsExtensions;

namespace LMS.Test;

public class UserTests
{
    private readonly IUnitOfWork _context = Substitute.For<IUnitOfWork>();

    [Fact]
    public async Task GetByIdAsync_ShouldReturenUser_WhenUserExist()
    {
        // Arrange
        var user = new User
        {
            UserId = 1, Name = "Ahmed", ContactInformation = "ahmed@gmail.com", LibraryCardNumber = "a2wd"
        };
        _context.Users.GetByIdAsync(1).Returns(user);
        var handler = new GetUserByIdQueryHandler(_context);

        // Act
        var result = await handler.Handle(new GetUserByIdQuery(1), CancellationToken.None);

        // Assert

        result.Should().BeEquivalentTo(user.Adapt<UserResponse>());
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturenNull_WhenUserNotExist()
    {
        // Arrange
        _context.Users.GetByIdAsync(Arg.Any<int>()).ReturnsNull();
        var handler = new GetUserByIdQueryHandler(_context);

        // Act
        var result = await handler.Handle(new GetUserByIdQuery(1), CancellationToken.None);

        // Assert
        Assert.Null(result);
    }

    [Theory]
    [InlineData(1, 5)]
    [InlineData(2, 3)]
    [InlineData(3, 2)]
    [InlineData(0, 0)]
    [InlineData(-1, -1)]
    public async Task GetAllAsync_ShouldReturnAllUsers(int pageNumber, int pageSize)
    {
        // Arrange
        var users = new List<User>
        {
            new User
            {
                UserId = 1, Name = "Ahmed", ContactInformation = "ahmed@gmail.com", LibraryCardNumber = "a2wd"
            },
            new User { UserId = 2, Name = "Ali", ContactInformation = "ali@gmail.com", LibraryCardNumber = "a3wd" },
            new User { UserId = 3, Name = "Mohamed", ContactInformation = "mohammed", LibraryCardNumber = "a4wd" }
        };
       // var pagination = new PaginationQuery { pageNumber = pageNumber, pageSize = pageSize };
        var filter = new PaginationFilter(pageSize, pageNumber);
        var query = new GetAllUsersQuery(filter);

        _context.Users.GetAllAsync(CancellationToken.None, query.PaginationQuery.pageSize, query.PaginationQuery.pageNumber).Returns(users);
        var handler = new GetAllUsersQueryHandler(_context);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);


        // Assert
            result.Should().BeEquivalentTo(users.Adapt<List<UserResponse>>());
    }

    [Fact]
    public async Task CreateUserCommandHandler_ShouldReturnUserResponse_WhenUserAdded()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Name = "Ahmed", ContactInformation = "ahmed@gmail,com"
        };
        
        var user = new User
        {
            Name = "Ahmed", ContactInformation = "ahmed@gmail,com"
        };
        await _context.Users.AddAsync(command.Adapt<User>());

        var handler = new CreateUserCommandHandler(_context);
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.Equal(user.Name, result.Name);
        Assert.Equal(user.ContactInformation, result.ContactInformation);
        result.LibraryCardNumber.Should().NotBeNullOrWhiteSpace();
    }
    
    [Fact]
    public  void CreateCommandValidator_ShouldThrowValidationException_WhenCommandIsInvalid()
    {
        // Arrange
        var validator = new CreateUserCommandValidator();
        var command = new CreateUserCommand
        {
            Name = "", // Invalid name
            ContactInformation = "" // Invalid email
        };

        // Act
        Action act = () => validator.ValidateAndThrow(command);

        // Assert
        act.Should().Throw<ValidationException>()
            .And.Message.Should().Contain("Name")
            .And.Contain("ContactInformation");
    }
    [Fact]
    public  void UpdateCommandValidator_ShouldThrowValidationException_WhenCommandIsInvalid()
    {
        // Arrange
        var validator = new UpdateUserCommandValidator();
        var command = new UpdateUserCommand()
        {
            Name = "", // Invalid name
            ContactInformation = "" // Invalid email
        };

        // Act
        Action act = () => validator.ValidateAndThrow(command);

        // Assert
        act.Should().Throw<ValidationException>()
            .And.Message.Should().Contain("Name")
            .And.Contain("ContactInformation");
    }
    
    
    
    [Fact]
    public async Task UpdateCommandHandler_ShouldReturnUserResponse_WhenUserUpdated()
    {
        // Arrange
        var command = new UpdateUserCommand
        {
            UserId = 1, Name = "Ahmed barq", ContactInformation = "ahmed@gmail,com"
        };
        
        var user = new User
        {
            UserId = 1, Name = "Ahmed", ContactInformation = "ahmed@gmail,com"
        };
        _context.Users.GetByIdAsync(1).Returns(user);
        await _context.Users.Update(user);
        var handler = new UpdateUserCommandHandler(_context);
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(command.Adapt<UserResponse>());
    }
    
    [Fact]
    public async Task DeleteCommandHandler_ShouldReturnUserResponse_WhenUserDeleted()
    {
        // Arrange
        var command = new DeleteUserCommand(1);
       
        
        var user = new User
        {
            UserId = 1, Name = "Ahmed", ContactInformation = "ahmed@gmail,com", LibraryCardNumber = "a2wd"
        };
        _context.Users.GetByIdAsync(1).Returns(user);
        await _context.Users.Delete(user);
        var handler = new DeleteUserCommandHandler(_context);
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(user.Adapt<UserResponse>());
    }
    [Fact]
    public async Task DeleteCommandHandler_ShouldReturnNull_WhenUserNotExist()
    {
        // Arrange
        var command = new DeleteUserCommand(1);
        _context.Users.GetByIdAsync(1).ReturnsNull();
        var handler = new DeleteUserCommandHandler(_context);
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().BeNull();
    }
}
