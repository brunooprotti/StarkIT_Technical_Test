
using FluentValidation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StarkIT.API.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { error = ex.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new { error = new List<string>() { ex.Message } });
                _logger.LogError("{@endpoint} - An unexpected error occurred" , context.GetEndpoint());
            }
        }
    }
}
