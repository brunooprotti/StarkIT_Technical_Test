using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace StarkIT.Application.Behaviours
{
    public class LoggingPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> _logger;

        public LoggingPipelineBehaviour(ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                var result = await next();

                stopwatch.Stop();
                
                _logger.LogInformation("LoggingPipelineBehaviour - Completed request {@RequestName} in {@stopwatch}s", typeof(TRequest).Name, stopwatch.ElapsedMilliseconds);

                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                
                _logger.LogError(ex, "LoggingPipelineBehaviour - Request {@RequestName} failed in {@stopwatch}s", typeof(TRequest).Name, stopwatch.ElapsedMilliseconds);
                throw;
            }
        }
    }
}
