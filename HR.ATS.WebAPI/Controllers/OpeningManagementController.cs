using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using HR.ATS.Command.Opening;
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
    public class OpeningManagementController: TnfController
    {
        private readonly IMediator _mediator;

        public OpeningManagementController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OpeningDTO>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(TotvsErrorMessage), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllOpenings(string? filter)
        {
            var result = await _mediator.Send(new GetAllOpeningsQuery(filter));
            return CreateResponseOnGetAll(result);
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<OpeningDTO>), (int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(TotvsErrorMessage), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateOpening([FromBody] OpeningDTO openingDto)
        {
            var result = await _mediator.Send(new CreateOpeningCommand(openingDto));
            return CreateResponseOnPost(result);
        }
    }
}