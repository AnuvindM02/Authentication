using System.Security.Cryptography;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace Authentication.Infrastructure.Services
{
    public class KeyRotationService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _rotationInterval = TimeSpan.FromDays(7);

        public KeyRotationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await RotateKeysAsync();
                await Task.Delay(_rotationInterval, cancellationToken);
            }
        }
        private async Task RotateKeysAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var activeKey = await context.SigningKeys.FirstOrDefaultAsync(k => k.IsActive);

            if (activeKey == null || activeKey.ExpiresAt <= DateTime.UtcNow.AddDays(10))
            {
                if (activeKey != null)
                {
                    activeKey.IsActive = false;
                    context.SigningKeys.Update(activeKey);
                }

                using var rsa = RSA.Create(2048);
                var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
                var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());

                var newKeyId = Guid.NewGuid().ToString();

                var newKey = new SigningKey
                {
                    PrivateKey = privateKey,
                    PublicKey = publicKey,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddYears(1),
                    KeyId = newKeyId
                };

                await context.SigningKeys.AddAsync(newKey);
                await context.SaveChangesAsync();
            }
        }
    }
}
