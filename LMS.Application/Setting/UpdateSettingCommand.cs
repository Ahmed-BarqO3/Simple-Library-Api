using System.Data;
using FluentValidation;
using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;
using Microsoft.Data.SqlClient;

namespace LMS.Application.Setting;

public record UpdateSettingCommand(SettingResponse Setting) : IRequest<SettingResponse>;


public class UpdateSettingCommandHandler(IUnitOfWork context) : IRequestHandler<UpdateSettingCommand, SettingResponse>
{
    public  ValueTask<SettingResponse> Handle(UpdateSettingCommand request, CancellationToken cancellationToken)
    {
        var setting = new Core.Models.Setting
        {
            DefualtBorrrowDays = request.Setting.DefualtBorrrowDays,
            DefaultFinePerDay = request.Setting.DefaultFinePerDay
        };

        

        return ValueTask.FromResult(context.Settings
            .ExecuteStoredProcTask($"SP_UpdateSetting {setting.DefaultFinePerDay}, {setting.DefualtBorrrowDays}")
            .Adapt<SettingResponse>());
    }
}




public class UpdateSettingCommandValidator : AbstractValidator<UpdateSettingCommand>
{
    public UpdateSettingCommandValidator()
    {
        RuleFor(x => x.Setting.DefaultFinePerDay).GreaterThan(0);
    }
}
