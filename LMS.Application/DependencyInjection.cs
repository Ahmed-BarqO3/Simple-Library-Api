using FluentValidation;
using LMS.Application.Common;
using LMS.Application.Interface;
using Mediator;
using Microsoft.AspNetCore.Http;
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
        
        
        services.AddScoped<IUriService>(provider =>
        {
            var accessor = provider.GetRequiredService<IHttpContextAccessor>();
            var request = accessor.HttpContext?.Request;
            var uri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent());
            return new UriService(uri);
        });
        

        return services;
    }
}
