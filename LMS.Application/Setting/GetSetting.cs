using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.Setting;

public record GetSetting( ) : IRequest<SettingResponse>;



class GetSettingHandler(IUnitOfWork context) : IRequestHandler<GetSetting, SettingResponse>
{
    public async ValueTask<SettingResponse> Handle(GetSetting request, CancellationToken cancellationToken)
    {
        var setting = context.Settings.GetAllAsync(cancellationToken).Result.FirstOrDefault();

        return setting.Adapt<SettingResponse>();
    }
}
