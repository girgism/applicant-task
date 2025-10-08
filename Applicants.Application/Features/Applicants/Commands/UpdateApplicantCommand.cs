using Applicants.Domain.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Applicants.Application.Features.Applicants.Commands;

public class UpdateApplicantCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public AddApplicantsDto ApplicantDto { get; set; }

    private sealed class UpdateApplicantCommandHandler : IRequestHandler<UpdateApplicantCommand, Result<string>>
    {
        private readonly IApplicantsContext _context;

        public UpdateApplicantCommandHandler(IApplicantsContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(UpdateApplicantCommand request, CancellationToken cancellationToken)
        {
            var existingApplicant = await _context.Applicants.AsTracking()
                                                             .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existingApplicant is null)
            {
                return Result.Failure<string>("Applicant not found");
            }
            existingApplicant.Update(request.ApplicantDto.Name,
                                       request.ApplicantDto.FamilyName,
                                       request.ApplicantDto.Address,
                                       request.ApplicantDto.CountryOfOrigin,
                                       request.ApplicantDto.EmailAddress,
                                       request.ApplicantDto.Age,
                                       request.ApplicantDto.Hired);

            var saveResult = await _context.SaveChangesAsyncWithResult();

            if (!saveResult.IsSuccess)
            {
                return Result.Failure<string>(saveResult.Error);
            }

            return Result.Success<string>("Applicant updated successfully");
        }
    }
}
