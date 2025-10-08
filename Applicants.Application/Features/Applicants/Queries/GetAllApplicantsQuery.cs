using Applicants.Domain.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Applicants.Application.Features.Applicants.Queries;

public class GetAllApplicantsQuery : IRequest<Result<List<ApplicantsDataDto>>>
{
    public int Skip { get; set; }
    public int Take { get; set; }
    private sealed class GetAllApplicantsQueryHandler : IRequestHandler<GetAllApplicantsQuery, Result<List<ApplicantsDataDto>>>
    {
        private readonly IApplicantsContext _context;

        public GetAllApplicantsQueryHandler(IApplicantsContext context)
        {
            _context = context;
        }

        public async Task<Result<List<ApplicantsDataDto>>> Handle(GetAllApplicantsQuery request, CancellationToken cancellationToken)
        {
            var applicants = await _context.Applicants
                .Select(a => new ApplicantsDataDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    FamilyName = a.FamilyName,
                    Address = a.Address,
                    CountryOfOrigin = a.CountryOfOrigin,
                    EmailAddress = a.EmailAddress,
                    Age = a.Age,
                    Hired = a.Hired,
                    IsDeleted = a.IsDeleted
                }).Skip((request.Skip))
                  .Take(request.Take)
                  .ToListAsync();

            return Result.Success(applicants);
        }
    }
}
