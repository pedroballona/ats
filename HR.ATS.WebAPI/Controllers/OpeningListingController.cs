using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using HR.ATS.Command.Applicant;
using HR.ATS.CrossCutting.Dto.Opening;
using HR.ATS.Query.Opening;
using HR.ATS.WebAPI.Configurations;
using HR.ATS.WebAPI.Security.Roles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.ATS.WebAPI.Controllers
{
    [Route("api/opening/listing")]
    [TnfRoleAuthorize(RolesConstants.AtsCandidate)]
    public class OpeningListingController: TnfController
    {
        private readonly IMediator _mediator;

        public OpeningListingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OpeningDTO>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(TotvsErrorMessage), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllOpenings(string? filter)
        {
            var result = await _mediator.Send(new GetAllOpeningsQuery(filter));
            return Ok(result);
        }
        
        [HttpPost("{id:guid}/apply")]
        [ProducesResponseType(typeof(ApplicationDTO), (int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(TotvsErrorMessage), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllOpenings(Guid id)
        {
            var result = await _mediator.Send(new ApplyLoggedApplicantToOpeningCommand(id));
            return CreateResponseOnPost(result);
        }
        
    }
}