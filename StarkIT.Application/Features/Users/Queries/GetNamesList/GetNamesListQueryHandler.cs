using MediatR;
using Microsoft.Extensions.Logging;
using StarkIT.Application.Contracts.Persistence;
using StarkIT.Application.Exceptions;
using StarkIT.Domain.Models;

namespace StarkIT.Application.Features.Users.Queries.GetNamesList
{
    public class GetNamesListQueryHandler : IRequestHandler<GetNamesListQuery, ICollection<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetNamesListQueryHandler> _logger;

        public GetNamesListQueryHandler(IUserRepository userRepository, ILogger<GetNamesListQueryHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<ICollection<User>> Handle(GetNamesListQuery request, CancellationToken cancellationToken)
        {
            //logica de consulta a bdd
            var namesList = await _userRepository.GetAll();

            // return del Mapeo de datos para devolver un DTO
            return namesList;
        }
    }
}
