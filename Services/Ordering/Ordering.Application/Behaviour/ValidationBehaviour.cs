namespace Ordering.Application.Behaviour;

using FluentValidation;
using MediatR;

//This will collect fluent validators and run before handler
public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators = validators;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);
        //This will run all the validation rules one by one and returns the validation result
        var validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        //now need to check for any failure
        var failures = validationResults.SelectMany(e => e.Errors).Where(f => f != null).ToList();
        if(failures.Count!=0)
        {
            throw new ValidationException(failures);
        }
        //On success case 
        return await next();
    }
}