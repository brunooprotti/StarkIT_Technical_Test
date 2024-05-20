using MediatR;
using System.Linq.Expressions;
using StarkIT.Domain.Models;

namespace StarkIT.Application.Features.Users.Queries.GetNamesList
{
    public class GetNamesListQuery : IRequest<ICollection<User>> { }
}
