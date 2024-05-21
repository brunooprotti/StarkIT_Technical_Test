using MediatR;
using Microsoft.Extensions.Logging;
using StarkIT.Application.Contracts.Persistence;
using StarkIT.Application.Exceptions;
using StarkIT.Application.Features.Name.Queries.GetNamesList;
using StarkIT.Domain.Models;

namespace StarkIT.Application.Features.Name.Queries.GetNamesList
{
    public class GetNamesListQueryHandler : IRequestHandler<GetNamesListQuery, ICollection<Names>>
    {
        private readonly INameRepository _nameRepository;
        private readonly ILogger<GetNamesListQueryHandler> _logger;

        public GetNamesListQueryHandler(INameRepository nameRepository, ILogger<GetNamesListQueryHandler> logger)
        {
            _nameRepository = nameRepository;
            _logger = logger;
        }

        public async Task<ICollection<Names>> Handle(GetNamesListQuery request, CancellationToken cancellationToken)
        {
            //logica de consulta a bdd
            var namesList = await _nameRepository.GetAll();

            // return del Mapeo de datos para devolver un DTO
            return namesList;
        }
    }
}
