using Applicants.Application.Features.Applicants.Commands;
using FluentValidation;

namespace Applicants.Application.Features.Applicants.Validators;

public class UpdateApplicantValidator : AbstractValidator<UpdateApplicantCommand>
{
    public UpdateApplicantValidator()
    {
        RuleFor(x => x.ApplicantDto.Name)
                   .NotEmpty()
                   .WithMessage("Name is required")
                   .MinimumLength(5)
                   .WithMessage("Name must be at least 6 characters")
                   .MaximumLength(150)
                   .WithMessage("Name must not exceed 50 characters");

        RuleFor(x => x.ApplicantDto.FamilyName)
                   .NotEmpty()
                   .WithMessage("Family Name is required")
                   .MinimumLength(5)
                   .WithMessage("Family Name must be at least 6 characters")
                   .MaximumLength(150)
                   .WithMessage("Family Name must not exceed 50 characters");

        RuleFor(x => x.ApplicantDto.Address)
                   .NotEmpty()
                   .WithMessage("Address is required")
                   .MinimumLength(10)
                   .WithMessage("Address must be at least 6 characters")
                   .MaximumLength(250)
                   .WithMessage("Address must not exceed 50 characters");


        RuleFor(x => x.ApplicantDto.EmailAddress)
                  .NotEmpty()
                  .WithMessage("Address is required")
                  .EmailAddress()
                  .WithMessage("Must be valid email address");

        RuleFor(x => x.ApplicantDto.Age)
                  .NotEmpty().WithMessage("Age is required")
                  .GreaterThan(20).LessThan(60).WithMessage("Age must between 20 to 60");

        RuleFor(x => x.ApplicantDto.Hired)
                  .NotNull().WithMessage("Hired shouldn't be null");
    }
}
