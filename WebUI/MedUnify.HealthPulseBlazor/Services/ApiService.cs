namespace MedUnify.HealthPulseBlazor.Services
{
    using MedUnify.ResourceModel.Auth;
    using System.Net.Http.Json;

    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TokenResponseResourceModel> GetTokenAsync(TokenRequestResourceModel requestModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/GetToken", requestModel);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TokenResponseResourceModel>();
        }
    }
}