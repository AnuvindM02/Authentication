using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Authentication.Application.Interfaces.Repositories;
using Authentication.Application.Interfaces.Services;
using Authentication.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Infrastructure.Services
{
    public class JwtTokenService(ISigningKeyRepository _signingKeyRepository,
        IConfiguration _configuration) : IJwtTokenService
    {
        public async Task<string> GenerateToken(User user, Client client, CancellationToken cancellationToken)
        {
            var signingKey = await _signingKeyRepository.GetActiveSigningKey(cancellationToken);
            if (signingKey == null)
                throw new Exception("No active signing key available");

            var privateKeyBytes = Convert.FromBase64String(signingKey.PrivateKey);

            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(privateKeyBytes, out _);

            var rsaSecurityKey = new RsaSecurityKey(rsa)
            {
                KeyId = signingKey.KeyId
            };

            var creds = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach (var userRole in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
            }

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: client.ClientURL,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
                );

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.WriteToken(tokenDescriptor);

            return token;
        }
    }
}
