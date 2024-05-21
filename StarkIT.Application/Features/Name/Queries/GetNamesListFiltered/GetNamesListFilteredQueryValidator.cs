using FluentValidation;
using System.Text.RegularExpressions;

namespace StarkIT.Application.Features.Name.Queries.GetNamesListFiltered
{
    public class GetNamesListFilteredQueryValidator : AbstractValidator<GetNamesListFilteredQuery>
    {
        public GetNamesListFilteredQueryValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(50).WithMessage("Name field exceeds maximum length (50)")
                .Must(name => Regex.IsMatch(name, @"^[a-zA-Z]+$")).WithMessage("Name can only contain letters.");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Gender field has to be Enum type");
        }
    }
}
