﻿using Authentication.Domain.Entities;
namespace Authentication.Application.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task<Client?> GetFirst(CancellationToken cancellationToken);
        Task<Client?> GetByClientIdAsync(string clientId, CancellationToken cancellationToken);
    }
}
