using FluentValidation;
using FluentValidation.Results;
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
        public GetNamesListFilteredQuery(string name, string? gender)
        {
            Name = name.ToUpper();
            Gender = Enum.TryParse<Gender>(gender, out var parsedGender) ? parsedGender : 
                throw new ValidationException("",[ new ValidationFailure("Gender", "Gender field has to be Enum type")] );
            
            _Expression = x => x.Name.ToUpper().StartsWith(Name) && x.Gender == Gender;
        }
    }
}
