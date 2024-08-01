namespace MedUnify.HealthPulseAPI.Services.Interface
{
    using MedUnify.Domain.Auth;

    public interface IOAuthClientService
    {
        Task<OAuthClient> GetClientByClientIdAsync(string clientId);
    }
}