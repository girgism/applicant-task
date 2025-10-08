using Applicants.Domain.Entities;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Applicants.Domain.Interfaces;

public interface IApplicantsContext
{
    DbSet<Applicant> Applicants { get; set; }
    Task<Result> SaveChangesAsyncWithResult();
}
