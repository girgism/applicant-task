using Applicants.Domain.Entities;
using Applicants.Domain.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;

namespace Applicants.Application.Features.Applicants.Commands;

public class AddApplicantCommand : IRequest<Result<string>>
{
    public AddApplicantsDto ApplicantDto { get; set; }

    private sealed class AddApplicantCommandHandler : IRequestHandler<AddApplicantCommand, Result<string>>
    {
        private readonly IApplicantsContext _context;

        public AddApplicantCommandHandler(IApplicantsContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(AddApplicantCommand request, CancellationToken cancellationToken)
        {
            var instance = Applicant.Instance(request.ApplicantDto.Name,
                                              request.ApplicantDto.FamilyName,
                                              request.ApplicantDto.Address,
                                              request.ApplicantDto.CountryOfOrigin,
                                              request.ApplicantDto.EmailAddress,
                                              request.ApplicantDto.Age,
                                              request.ApplicantDto.Hired);

            _context.Applicants.Add(instance);
            var saveResult = await _context.SaveChangesAsyncWithResult();

            if (!saveResult.IsSuccess)
            {
                return Result.Failure<string>(saveResult.Error);
            }

            return Result.Success<string>("Applicant added successfully");
        }
    }
}
