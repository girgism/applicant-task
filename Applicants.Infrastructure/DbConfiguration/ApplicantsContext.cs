using Applicants.Domain.Entities;
using Applicants.Domain.Interfaces;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Applicants.Infrastructure.DbConfiguration;

public class ApplicantsContext : IdentityDbContext<User,
                                                   Role,
                                                   int,
                                                   IdentityUserClaim<int>,
                                                   IdentityUserRole<int>,
                                                   IdentityUserLogin<int>,
                                                   IdentityRoleClaim<int>,
                                                   IdentityUserToken<int>>, IApplicantsContext
{

    public ApplicantsContext(DbContextOptions<ApplicantsContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicantsContext).Assembly);
    }

    public DbSet<Applicant> Applicants { get; set; }

    public async Task<Result> SaveChangesAsyncWithResult()
    {
        try
        {
            var result = await base.SaveChangesAsync();
            return Result.Success();

        }
        catch (Exception exp)
        {
            return Result.Failure(exp.Message);
        }
    }
}
