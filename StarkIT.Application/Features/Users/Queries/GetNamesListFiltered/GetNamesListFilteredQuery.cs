using MediatR;
using StarkIT.Domain.Models;
using System.Linq.Expressions;

namespace StarkIT.Application.Features.Users.Queries.GetNamesListFiltered
{
    public class GetNamesListFilteredQuery : IRequest<ICollection<User>>
    {
        public Expression<Func<User,bool>> _Expression { get; set; }
        public GetNamesListFilteredQuery(Expression<Func<User,bool>> Expression)
        {
            _Expression = Expression ?? throw new ArgumentNullException(nameof(Expression));
        }
    }
}
