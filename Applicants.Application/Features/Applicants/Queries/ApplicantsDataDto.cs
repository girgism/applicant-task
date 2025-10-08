namespace Applicants.Application.Features.Applicants.Queries;

public class ApplicantsDataDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string FamilyName { get; set; }

    public string Address { get; set; }

    public string CountryOfOrigin { get; set; }

    public string EmailAddress { get; set; }
    public int Age { get; set; }

    public bool Hired { get; set; }
    public bool IsDeleted { get; set; }
}
