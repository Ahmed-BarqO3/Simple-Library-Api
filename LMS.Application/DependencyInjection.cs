using FluentValidation;
using Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator(options =>
        {
            options.Namespace = "LMS.Application";
            options.ServiceLifetime = ServiceLifetime.Transient;
        });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PipelineBehavior.ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}
