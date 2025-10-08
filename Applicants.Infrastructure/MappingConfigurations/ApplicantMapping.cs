using Applicants.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Applicants.Infrastructure.MappingConfigurations;

public class ApplicantMapping : IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder.ToTable("IT_Applicants");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(150).IsRequired();
        builder.Property(x => x.FamilyName).HasMaxLength(150).IsRequired();
        builder.Property(x => x.Address).HasMaxLength(300).IsRequired();
        builder.Property(x => x.CountryOfOrigin).HasMaxLength(150).IsRequired();
        builder.Property(x => x.EmailAddress).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Age).IsRequired();
        builder.Property(x => x.Hired).IsRequired();
        builder.Property(x => x.IsDeleted).HasDefaultValue(false);
    }
}
