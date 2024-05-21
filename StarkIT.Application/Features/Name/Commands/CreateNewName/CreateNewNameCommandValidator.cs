using FluentValidation;

namespace StarkIT.Application.Features.Name.Commands.CreateNewName
{
    public class CreateNewNameCommandValidator : AbstractValidator<CreateNewNameCommand>
    {
        public CreateNewNameCommandValidator()
        {
            RuleFor( x=> x.Name)
                .NotNull().WithMessage("Name field can't be null")
                .NotEmpty().WithMessage("Name field can't be empty")
                .MaximumLength(50).WithMessage("Name field exceeds maximum length (50)")
                .Matches("^[a-zA-Z]*$").WithMessage("Name can only contain letters.");

            RuleFor( x=> x.Gender)
                .NotEmpty().WithMessage("Gender field can't be empty")
                .NotNull().WithMessage("Gender field can't be null")
                .IsInEnum().WithMessage("Gender field has to be Enum type");
        }
    }
}
