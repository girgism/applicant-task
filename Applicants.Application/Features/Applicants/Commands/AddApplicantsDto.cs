namespace Applicants.Application.Features.Applicants.Commands;

public record AddApplicantsDto(
    string Name,
    string FamilyName,
    string EmailAddress,
    string Address,
    string CountryOfOrigin,
    int Age,
    bool Hired
);
