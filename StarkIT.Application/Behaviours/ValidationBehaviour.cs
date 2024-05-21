using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace StarkIT.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidationBehaviour<TRequest,TResponse>> _logger;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehaviour<TRequest,TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                if (_validators.Any())
                {
                    var context = new ValidationContext<TRequest>(request); //Le paso la request que me permite crear un contexto y ver si coincide alguno de los validadores que cree con la TRequest 

                    var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken))); //busca y ejecuta todas las validaciones antes de que lleguen a hacerse las peticiones.

                    var errorList = validationResults.SelectMany(v => v.Errors).Where(e => e != null).ToList(); //selecciono todos los errores y si son distintos de null los agrupo

                    if (errorList.Any())
                    {
                        throw new ValidationException(errorList);
                    }
                }
                return await next();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ValidationBehaviour: One or more validators errors were found");
                throw;
            }

        }
    }
}
