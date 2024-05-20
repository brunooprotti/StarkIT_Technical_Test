using MediatR;
using Microsoft.Extensions.Logging;

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
            _logger.LogInformation("Starting request {@RequestName} - {@DateTimeUtc}", typeof(TRequest).Name, DateTime.UtcNow);

            TResponse result;

            try
            {
                result = await next();
                _logger.LogInformation("Completed request {@RequestName} - {@DateTimeUtc}", typeof(TRequest).Name, DateTime.UtcNow);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Request {@RequestName} failed - {@DateTimeUtc}", typeof(TRequest).Name, DateTime.UtcNow);
                throw;
            }
        }
    }
}
