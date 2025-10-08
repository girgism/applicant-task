namespace Applicants.Domain.Entities;

public class Applicant
{
    public int Id { get; private set; } = default;

    public string Name { get; private set; } = string.Empty;

    public string FamilyName { get; private set; } = string.Empty;

    public string Address { get; private set; } = string.Empty;

    public string CountryOfOrigin { get; private set; } = string.Empty;

    public string EmailAddress { get; private set; } = string.Empty;
    public int Age { get; private set; } = default;

    public bool Hired { get; private set; } = default;

    public bool IsDeleted { get; private set; } = default;

    public static Applicant Instance(string name,
                                     string familyName,
                                     string address,
                                     string countryOfOrigin,
                                     string emailAddress,
                                     int age,
                                     bool hired)
    {
        return new Applicant
        {
            Name = name,
            FamilyName = familyName,
            Address = address,
            CountryOfOrigin = countryOfOrigin,
            EmailAddress = emailAddress,
            Age = age,
            Hired = hired,
            IsDeleted = false
        };
    }

    public void Update(string name,
                       string familyName,
                       string address,
                       string countryOfOrigin,
                       string emailAddress,
                       int age,
                       bool hired)
    {
        Name = name;
        FamilyName = familyName;
        Address = address;
        CountryOfOrigin = countryOfOrigin;
        EmailAddress = emailAddress;
        Age = age;
        Hired = hired;
    }

    public void Delete()
    {
        IsDeleted = true;
    }
}
