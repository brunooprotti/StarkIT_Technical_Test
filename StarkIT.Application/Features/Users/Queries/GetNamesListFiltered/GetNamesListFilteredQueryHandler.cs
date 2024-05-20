using MediatR;
using Microsoft.Extensions.Logging;
using StarkIT.Application.Contracts.Persistence;
using StarkIT.Application.Exceptions;
using StarkIT.Application.Features.Users.Queries.GetNamesList;
using StarkIT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarkIT.Application.Features.Users.Queries.GetNamesListFiltered
{
    public class GetNamesListFilteredQueryHandler : IRequestHandler<GetNamesListFilteredQuery,ICollection<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetNamesListFilteredQueryHandler> _logger;

        public GetNamesListFilteredQueryHandler(IUserRepository userRepository, ILogger<GetNamesListFilteredQueryHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<ICollection<User>> Handle(GetNamesListFilteredQuery request, CancellationToken cancellationToken)
        {
            //logica de consulta a bdd, mapearia los datos aca pero no es necesario ya que la app es simple.
            var namesList = await _userRepository.Get(request._Expression);

            return namesList;
        }
    }

}
