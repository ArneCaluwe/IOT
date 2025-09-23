using Microsoft.Extensions.Logging;

namespace MyIOTPoc.Business.QueryHandlers;

/// <summary>
/// Base class for query handlers that need to create activities for tracing.
/// </summary>
/// <param name="activitySource"></param>
/// <param name="logger"></param>
public abstract class QueryHandlerWithActivity<TRequest, TResponse>(ActivitySource activitySource, ILogger logger)
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

        Log.HandlingRequest(logger, GetType().Name);

        var response = HandleRequest(request, cancellationToken);

        Log.RequestCompleted(logger, GetType().Name);

        activity?.SetStatus(ActivityStatusCode.Ok);
        return response;
    }

    /// <summary>
    /// Handles the <typeparamref name="TRequest"/> and returns a response of type <typeparamref name="TResponse"/>.
    /// </summary>
    /// <param name="request">A Mediatr request of type <typeparamref name="TRequest"/> </param>
    /// <param name="cancellationToken">A Cancellation token which will cancell the pending actions</param>
    /// <returns></returns>
    public abstract Task<TResponse> HandleRequest(TRequest request, CancellationToken cancellationToken);
}

/// <summary>
/// Logger messages for QueryHandlerWithActivity.
/// </summary>
public partial class Log
{
    /// <summary>
    /// Logs information about handling a new request.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="handler"></param>
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Information,
        Message = "Handling a new request in {handler}")]
    public static partial void HandlingRequest(ILogger logger, string handler);

    /// <summary>
    /// Compile time logger method.
    /// Logs information about the completion of a request.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="handler"></param>
    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Information,
        Message = "Request completed in {handler}")]
    public static partial void RequestCompleted(ILogger logger, string handler);
}