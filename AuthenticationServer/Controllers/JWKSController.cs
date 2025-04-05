using Authentication.Application.Features.GetJWKS;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace Authentication.API.Controllers
{
    [Route(".well-known")]
    [ApiController]
    public class JWKSController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("jwks.json")]
        public async Task<IActionResult> GetJWKS(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetJWKSRequest(), cancellationToken);
            return Ok(new {keys = response});
        }
    }
}
