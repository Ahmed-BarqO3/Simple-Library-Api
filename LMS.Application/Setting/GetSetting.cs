using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.Setting;

public record GetSetting( ) : IRequest<SettingResponse>;

public class GetSettingHandler(IUnitOfWork context) : IRequestHandler<GetSetting, SettingResponse>
{
    public ValueTask<SettingResponse> Handle(GetSetting request, CancellationToken cancellationToken)
    {
        var setting =  context.Settings.GetAllAsync(cancellationToken).Result.FirstOrDefault();

        return  ValueTask.FromResult(setting.Adapt<SettingResponse>());
    }
}
