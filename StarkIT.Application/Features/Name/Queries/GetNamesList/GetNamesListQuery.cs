using MediatR;
using System.Linq.Expressions;
using StarkIT.Domain.Models;

namespace StarkIT.Application.Features.Name.Queries.GetNamesList
{
    public class GetNamesListQuery : IRequest<ICollection<Names>> { }
}
