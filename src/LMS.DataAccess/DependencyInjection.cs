using LMS.Application.Interface;
using LMS.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddTransient<IUnitOfWork,UnitOfWork>();

        return services;
    }
}
