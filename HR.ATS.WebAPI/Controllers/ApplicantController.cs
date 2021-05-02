using System.Net;
using System.Threading.Tasks;
using HR.ATS.Command.Applicant;
using HR.ATS.CrossCutting.Dto.Applicant;
using HR.ATS.Query;
using HR.ATS.Query.Applicant;
using HR.ATS.WebAPI.Configurations;
using HR.ATS.WebAPI.Security.Roles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.ATS.WebAPI.Controllers
{
    [Route("api/applicants")]
    [TnfRoleAuthorize(RolesConstants.AtsCandidate)]
    public class ApplicantController : TnfController
    {
        private readonly IMediator _mediator;

        public ApplicantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("logged/resume")]
        [ProducesResponseType(typeof(ResumeDTO), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(TotvsErrorMessage), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateResumeFromLoggedApplicant([FromBody] ResumeDTO resumeDto)
        {
            var result = await _mediator.Send(new UpdateResumeForLoggedApplicantCommand(resumeDto));
            return CreateResponseOnPut(result);
        }

        [HttpGet("logged/resume")]
        [ProducesResponseType(typeof(ResumeDTO), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(TotvsErrorMessage), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetResumeFromLoggedUser()
        {
            var result = await _mediator.Send(new GetResumeFromLoggedUserQuery());
            return CreateResponseOnGet(result);
        }
    }
}