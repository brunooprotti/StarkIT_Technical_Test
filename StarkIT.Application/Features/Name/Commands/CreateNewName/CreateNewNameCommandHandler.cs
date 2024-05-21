using AutoMapper;
using MediatR;
using StarkIT.Application.Contracts.Persistence;
using StarkIT.Domain.Models;

namespace StarkIT.Application.Features.Name.Commands.CreateNewName
{
    public class CreateNewNameCommandHandler : IRequestHandler<CreateNewNameCommand, bool>
    {
        private readonly INameRepository _nameRepository;
        private readonly IMapper _mapper;

        public CreateNewNameCommandHandler(INameRepository nameRepository, IMapper mapper)
        {
            _nameRepository = nameRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateNewNameCommand request, CancellationToken cancellationToken)
        {
            var mappedObject = _mapper.Map<Names>(request);
            return await _nameRepository.Create(mappedObject);
        }
    }
}
