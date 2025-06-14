using System.Security.Claims;
using Authentication.Application.Features.CreateProfile;
using Authentication.Application.Features.GetAllUsers;
using Authentication.Application.Features.GetProfile;
using Authentication.Application.Features.UpdateProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController(IMediator _mediator) : ControllerBase
    {
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
        public async Task<ActionResult<UpdateProfileResponse>> UpdateProfile([FromBody] UpdateProfileRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("getAll")]
        [Authorize]
        public async Task<ActionResult<GetAllUsersResponse>> GetAllUsers([FromQuery] GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                return Unauthorized("Invalid or missing user ID in token.");
            }
            request.CurrentUserId = userId;
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
