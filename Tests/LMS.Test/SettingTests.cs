using FluentAssertions;
using FluentValidation;
using LMS.Application.Interface;
using LMS.Application.Response;
using LMS.Application.Setting;
using LMS.Core.Models;
using Mapster;
using NSubstitute;
using NSubstitute.Extensions;

namespace LMS.Test;

public class SettingTests
{
    private readonly IUnitOfWork _context = Substitute.For<IUnitOfWork>();
    [Fact]
    public async Task GetSettingHandler_ShouldReturnSettingResponse()
    {
        // Arrange
        var setting = new List<Setting>() { new Setting { DefaultFinePerDay = 2, DefualtBorrrowDays = 7 } };
        var query = new GetSetting();

         _context.Settings.GetAllAsync(CancellationToken.None).Returns(setting);
         var handler = new GetSettingHandler(_context);

         // Act
         var result = await handler.Handle(query, CancellationToken.None);
         // Assert
         result.Should().BeEquivalentTo(setting.FirstOrDefault().Adapt<SettingResponse>());
    }

    [Fact]
    public async Task UpdateSettingCommandHandler_ShouldReturnSettingResponse_WhenSettingUpdated()
    {
        // Arrange
        var setting = new List<Setting> { new Setting { DefaultFinePerDay = 2, DefualtBorrrowDays = 7 } };
        var command = new UpdateSettingCommand(setting.Adapt<SettingResponse>());

        _context.Settings.GetAllAsync(CancellationToken.None).Returns(setting);
        var handler = new UpdateSettingCommandHandler(_context);
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(setting.Adapt<SettingResponse>());
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void UpdateSettingCommandValidator_ShouldThrowValidationException_WhenDataInvalid( decimal fine)
    {
        // Arrange
        var setting = new Setting { DefaultFinePerDay = fine, DefualtBorrrowDays =  3 };
        var command = new UpdateSettingCommand(setting.Adapt<SettingResponse>());
            

        var validator = new UpdateSettingCommandValidator();
        
        // Act
        Action act = () => validator.ValidateAndThrow(command);
        
        // Assert
        act.Should().Throw<ValidationException>()
            .And.Message.Should().Contain("Setting Default Fine Per Day");
    }
}
