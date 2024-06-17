using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace LMS.Application.PipelineBehavior;
public static class AppBuilderExtensions
{
    public static void UseFluentValidationExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(x =>
        {

            x.Run(async context =>
            {
                var errorFeture = context.Features.Get<IExceptionHandlerFeature>();
                var exception = errorFeture.Error;

                if (!(exception is ValidationException validationException))
                {
                    throw exception;
                }
                var errors = validationException.Errors.Select(e => new
                {
                    e.PropertyName,
                    e.ErrorMessage
                });

                var errorText = JsonSerializer.Serialize(errors);
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(errorText);
            });
        });
    
    }
}
