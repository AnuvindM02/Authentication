using System.Security.Cryptography;
using Authentication.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication;

namespace Authentication.Application.Features.GetJWKS
{
    public sealed class GetJWKSHandler(ISigningKeyRepository _signingKeyRepository) : IRequestHandler<GetJWKSRequest, List<GetJWKSResponse>>
    {
        public async Task<List<GetJWKSResponse>> Handle(GetJWKSRequest request, CancellationToken cancellationToken)
        {
            var activeKeys = await _signingKeyRepository.GetAllByCondition(x => x.IsActive, cancellationToken)
                ?? throw new KeyNotFoundException("No active keys found");
            List<GetJWKSResponse> response = activeKeys.Select(x => new GetJWKSResponse
            {
                Kty = "RSA",
                Use = "sig",
                Kid = x.KeyId,
                Alg = "RS256",
                N = Base64UrlTextEncoder.Encode(GetModulus(x.PublicKey)),
                E = Base64UrlTextEncoder.Encode(GetExponent(x.PublicKey))
            }).ToList();
            return response;
        }

        private static byte[] GetModulus(string publicKey)
        {
            var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);
            var parameters = rsa.ExportParameters(false);
            rsa.Dispose();
            if (parameters.Modulus == null)
                throw new InvalidOperationException("RSA paramters are not valid");
            return parameters.Modulus;
        }

        private static byte[] GetExponent(string publicKey)
        {
            var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);
            var parameters = rsa.ExportParameters(false);
            rsa.Dispose();

            if (parameters.Exponent == null)
                throw new InvalidOperationException("RSA paramters are not valid");
            return parameters.Exponent;
        }
    }
}
