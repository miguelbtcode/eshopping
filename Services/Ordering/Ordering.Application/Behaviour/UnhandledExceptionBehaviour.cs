namespace Ordering.Application.Behaviour;

using MediatR;
using Microsoft.Extensions.Logging;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        this.logger = logger;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex) 
        {
            var requestName = typeof(TRequest).Name;
            this.logger.LogError(ex, "Unhandled exception occurred with Request Name: {RequestName}, Request: {Request}", requestName, request);
            throw;
        }
    }
}