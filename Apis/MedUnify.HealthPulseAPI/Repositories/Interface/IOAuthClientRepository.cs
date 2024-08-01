namespace MedUnify.HealthPulseAPI.Repositories
{
    using global::MedUnify.Domain.Auth;
    using MedUnify.HealthPulseAPI.DbContext;
    using Microsoft.EntityFrameworkCore;

    public interface IOAuthClientRepository
    {
        Task<OAuthClient> GetClientByClientIdAsync(string clientId);
    }
}