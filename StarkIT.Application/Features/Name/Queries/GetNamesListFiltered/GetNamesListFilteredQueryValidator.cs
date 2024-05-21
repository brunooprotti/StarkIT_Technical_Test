using FluentValidation;
using System.Text.RegularExpressions;

namespace StarkIT.Application.Features.Name.Queries.GetNamesListFiltered
{
    public class GetNamesListFilteredQueryValidator : AbstractValidator<GetNamesListFilteredQuery>
    {
        public GetNamesListFilteredQueryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .NotNull().WithMessage("Name field can't be null.")
                .MaximumLength(50).WithMessage("Name field exceeds maximum length (50)")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ]+$").WithMessage("Name can only contain letters.");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Gender field has to be Enum type");
        }
    }
}
