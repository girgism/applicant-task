using Applicants.Domain.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Applicants.Application.Features.Applicants.Commands;

public class DeleteApplicantCommand : IRequest<Result<string>>
{
    public int Id { get; set; }

    private sealed class DeleteApplicantCommandHandler : IRequestHandler<DeleteApplicantCommand, Result<string>>
    {
        private readonly IApplicantsContext _context;

        public DeleteApplicantCommandHandler(IApplicantsContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(DeleteApplicantCommand request, CancellationToken cancellationToken)
        {
            var existingApplicant = await _context.Applicants.AsTracking()
                                                            .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existingApplicant is null)
            {
                return Result.Failure<string>("Applicant not found");
            }
            existingApplicant.Delete();

            var saveResult = await _context.SaveChangesAsyncWithResult();

            if (!saveResult.IsSuccess)
            {
                return Result.Failure<string>(saveResult.Error);
            }

            return Result.Success<string>("Applicant deleted");
        }
    }
}
