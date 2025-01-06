namespace Ordering.Application.Exceptions;

using FluentValidation.Results;

public class ValidationException() : ApplicationException("One or more validation error(s) occurred.")
{
    public Dictionary<string, string[]> Errors { get; } = new();

    public ValidationException(IEnumerable<ValidationFailure> failures): this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, 
                     e => e.ErrorMessage)
            .ToDictionary(failure => failure.Key, 
                          failure => failure.ToArray());
    }
}