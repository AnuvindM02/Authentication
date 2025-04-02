using MediatR;

namespace Authentication.Application.Features.GetJWKS;
    public sealed record GetJWKSRequest:IRequest<List<GetJWKSResponse>>;
