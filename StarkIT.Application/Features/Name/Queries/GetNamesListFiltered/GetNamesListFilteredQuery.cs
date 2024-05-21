using MediatR;
using StarkIT.Domain.Models;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;

namespace StarkIT.Application.Features.Name.Queries.GetNamesListFiltered
{
    public class GetNamesListFilteredQuery : IRequest<ICollection<Names>>
    {
        public string? Name { get; set; } = string.Empty;
        public Gender Gender { get; set; }

        public Expression<Func<Names,bool>> _Expression { get; set; }
        public GetNamesListFilteredQuery(string name, Gender gender)
        {
            Name = name.ToUpper();
            Gender = gender;
            _Expression = x => x.Name.ToUpper().StartsWith(Name) && x.Gender == Gender;
        }
    }
}
