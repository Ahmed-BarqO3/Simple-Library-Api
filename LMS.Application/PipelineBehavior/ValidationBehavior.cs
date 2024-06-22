using FluentValidation;
using Mediator;

namespace LMS.Application.PipelineBehavior;
public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async ValueTask<TResponse> Handle(TRequest message, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
    {
        var context = new ValidationContext<TRequest>(message);

        var validationFailures = await Task.WhenAll(
            validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

        var errors = validators
                 .Select(x => x.Validate(context))
                 .SelectMany(x => x.Errors)
                 .Where(x => x != null);

        if (errors.Any())
        {
            throw new ValidationException(errors);
        }

        return await next(message, cancellationToken);
    }
}


