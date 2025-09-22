namespace MyIOTPoc.Business.QueryHandlers;

/// <summary>
/// Base class for query handlers that need to create activities for tracing.
/// </summary>
/// <param name="activitySource"></param>
public abstract class QueryHandlerWithActivity<TRequest, TResponse>(ActivitySource activitySource)
    : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// The activity source used to create activities for tracing.
    /// </summary>
    protected readonly ActivitySource _activitySource = activitySource;

    /// <inheritdoc />
    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        using var activity = _activitySource.StartActivity($"{GetType().Name}.Handle");
        activity?.AddTag("Handler", GetType().Name);

        var response = HandleRequest(request, cancellationToken);

        activity?.SetStatus(ActivityStatusCode.Ok);
        return response;
    }
    
    /// <summary>
    /// Handles the request and returns a response.
    /// </summary>
    /// <param name="request">A Mediatr request of type <typeparamref name="TRequest"/> </param>
    /// <param name="cancellationToken">A Cancellation token which will cancell the pending actions</param>
    /// <returns></returns>
    public abstract Task<TResponse> HandleRequest(TRequest request, CancellationToken cancellationToken);
}