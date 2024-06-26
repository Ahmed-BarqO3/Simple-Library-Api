using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.Setting;

public record UpdateSettingCommand(SettingResponse Setting) : IRequest<SettingResponse>;


public class UpdateSettingCommandHandler(IUnitOfWork context) : IRequestHandler<UpdateSettingCommand, SettingResponse>
{
    public async ValueTask<SettingResponse> Handle(UpdateSettingCommand request, CancellationToken cancellationToken)
    {
        var setting = new Core.Models.Setting
        {
            DefualtBorrrowDays = request.Setting.DefualtBorrrowDays,
            DefaultFinePerDay = request.Setting.DefaultFinePerDay
        };
        await context.Settings.Update(setting);
        context.Save();

        return setting.Adapt<SettingResponse>();
    }
}
