using Applicants.Application.Features.Applicants.Commands;
using Applicants.Application.Features.Applicants.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Applicants.APIs.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class ApplicantController : ControllerBase
{
    public IMediator _mediator { get; set; }

    public ApplicantController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApplicantsDataDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
    [Route("get-applicants")]
    public async Task<ActionResult> GetAllApplicantData(int skip = 0, int take = 10)
    {
        var response = await _mediator.Send(new GetAllApplicantsQuery()
        {
            Skip = skip,
            Take = take,
        });

        if (response.IsFailure)
        {
            return BadRequest(BaseResponse.CreateProblemDetail(response.Error));
        }
        return Ok(response.Value);

    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
    [Route("add-applicant")]
    public async Task<IActionResult> AddApplicant(AddApplicantsDto data)
    {
        var result = await _mediator.Send(new AddApplicantCommand
        {
            ApplicantDto = data
        });

        if (!result.IsSuccess)
        {
            return BadRequest(BaseResponse.CreateProblemDetail(result.Error));
        }

        return Ok(result.Value);
    }

    [HttpPut]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
    [Route("update-applicant")]
    public async Task<IActionResult> UpdateApplicant(int id, AddApplicantsDto data)
    {
        var result = await _mediator.Send(new UpdateApplicantCommand
        {
            Id = id,
            ApplicantDto = data
        });

        if (!result.IsSuccess)
        {
            return BadRequest(BaseResponse.CreateProblemDetail(result.Error));
        }

        return Ok(result.Value);
    }


    [HttpDelete]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
    [Route("delete-applicant")]
    public async Task<IActionResult> DeleteApplicant(int id)
    {
        var result = await _mediator.Send(new DeleteApplicantCommand
        {
            Id = id
        });

        if (!result.IsSuccess)
        {
            return BadRequest(BaseResponse.CreateProblemDetail(result.Error));
        }

        return Ok(result.Value);
    }
}
