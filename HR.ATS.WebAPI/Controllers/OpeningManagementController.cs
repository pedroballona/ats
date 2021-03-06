using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using HR.ATS.Command.Opening;
using HR.ATS.CrossCutting.Dto.Applicant;
using HR.ATS.CrossCutting.Dto.Opening;
using HR.ATS.Query.Applicant;
using HR.ATS.Query.Opening;
using HR.ATS.WebAPI.Configurations;
using HR.ATS.WebAPI.Security.Roles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.ATS.WebAPI.Controllers
{
    [Route("api/opening/management")]
    [TnfRoleAuthorize(RolesConstants.AtsRecruiter)]
    public class OpeningManagementController : TnfController
    {
        private readonly IMediator _mediator;

        public OpeningManagementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OpeningDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(TotvsErrorMessage), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllOpenings(string? filter)
        {
            var result = await _mediator.Send(new GetAllOpeningsQuery(filter));
            return Ok(result);
        }

        [HttpGet("{id:guid}/applied/applicants")]
        [ProducesResponseType(typeof(IEnumerable<SimpleApplicantDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(TotvsErrorMessage), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllOpenings(Guid id, [FromQuery] string? filter)
        {
            var result = await _mediator.Send(new GetAllApplicantsThatAppliedToAnOpeningQuery(id, filter));
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<OpeningDto>), (int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(TotvsErrorMessage), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateOpening([FromBody] OpeningDto openingDto)
        {
            var result = await _mediator.Send(new CreateOpeningCommand(openingDto));
            return CreateResponseOnPost(result);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(TotvsErrorMessage), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteOpening(Guid id)
        {
            await _mediator.Send(new DeleteOpeningCommand(id));
            return CreateResponseOnDelete();
        }

        [HttpGet("applicant/{id:guid}")]
        [ProducesResponseType(typeof(IEnumerable<ApplicantDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(TotvsErrorMessage), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllOpenings(Guid id)
        {
            var result = await _mediator.Send(new GetResumeFromApplicantQuery(id));
            return CreateResponseOnGet(result);
        }
    }
}