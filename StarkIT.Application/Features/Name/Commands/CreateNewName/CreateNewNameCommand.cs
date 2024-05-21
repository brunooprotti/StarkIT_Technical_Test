
using MediatR;
using StarkIT.Domain.Models;

namespace StarkIT.Application.Features.Name.Commands.CreateNewName
{
    public class CreateNewNameCommand : IRequest<bool>
    {
        public string Name { get; set; } = string.Empty;
        public Gender Gender { get; set; }
    }
}
