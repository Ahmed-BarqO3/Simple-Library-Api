using FluentValidation;
using Mediator;

namespace LMS.Application.PipelineBehavior;
public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async ValueTask<TResponse> Handle(TRequest message, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
    {
        var context = new ValidationContext<TRequest>(message);

        var validationResult = await validators.ValidateAsync(context, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        return await next(message, cancellationToken);
    }
}


