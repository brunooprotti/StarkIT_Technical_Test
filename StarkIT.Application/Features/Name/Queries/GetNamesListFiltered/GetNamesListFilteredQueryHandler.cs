using MediatR;
using Microsoft.Extensions.Logging;
using StarkIT.Application.Contracts.Persistence;
using StarkIT.Domain.Models;

namespace StarkIT.Application.Features.Name.Queries.GetNamesListFiltered
{
    public class GetNamesListFilteredQueryHandler : IRequestHandler<GetNamesListFilteredQuery,ICollection<Names>>
    {
        private readonly INameRepository _nameRepository;
        private readonly ILogger<GetNamesListFilteredQueryHandler> _logger;

        public GetNamesListFilteredQueryHandler(INameRepository nameRepository, ILogger<GetNamesListFilteredQueryHandler> logger)
        {
            _nameRepository = nameRepository;
            _logger = logger;
        }

        public async Task<ICollection<Names>> Handle(GetNamesListFilteredQuery request, CancellationToken cancellationToken)
        {
            var namesList = await _nameRepository.Get(request._Expression);

            return namesList;
        }
    }

}
