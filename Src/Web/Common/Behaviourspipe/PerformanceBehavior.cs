using System.Diagnostics;
using MediatR;

namespace Web.Common.Behaviourspipe;

public class PerformanceBehavior<TRequest,TResponse> :IPipelineBehavior<TRequest,TResponse> where TRequest:IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;
    private readonly Stopwatch _timer;

    public PerformanceBehavior(ILogger<TRequest> logger, Stopwatch timer)
    {
        _logger = logger;
        _timer = timer;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("performancen (3. for command) (4. for query)");
        _timer.Start();
        var response = await next();
        _timer.Stop();
        var elapsedMilliseconds = _timer.ElapsedMilliseconds;
        if (elapsedMilliseconds<=500)
        {
            return response;
        }

        var requestname = typeof(TRequest).Name;
        _logger.LogWarning("clean archetuecture long running request:{Name}({ElapsedMilliseconds} milliseconds) {@UserId}",requestname,elapsedMilliseconds,request);
        return response;
    }
}