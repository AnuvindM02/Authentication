using Authentication.Application.Features.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
