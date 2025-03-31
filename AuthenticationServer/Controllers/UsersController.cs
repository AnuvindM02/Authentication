using Authentication.Application.Features.CreateProfile;
using Authentication.Application.Features.GetProfile;
using Authentication.Application.Features.UpdateProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<CreateProfileResponse>> Register([FromBody] CreateProfileRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Created(nameof(Register), response);
        }

        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult<GetProfileResponse>> Get(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetProfileRequest(), cancellationToken);
            return Ok(response);
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<ActionResult<UpdateProfileResponse>> UpdateProfile([FromBody]UpdateProfileRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
