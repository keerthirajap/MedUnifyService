namespace MedUnify.HealthPulseBlazor.Services
{
    using Microsoft.JSInterop;
    using System.Threading.Tasks;

    public class AuthenticationService
    {
        private readonly ApiService _apiService;
        private readonly IJSRuntime _jsRuntime;

        public AuthenticationService(ApiService apiService, IJSRuntime jsRuntime)
        {
            _apiService = apiService;
            _jsRuntime = jsRuntime;
        }

        public async Task<bool> LoginAsync(string clientId, string clientSecret)
        {
            //var requestModel = new TokenRequestResourceModel
            //{
            //    ClientId = clientId,
            //    ClientSecret = clientSecret
            //};

            //var response = await _apiService.GetTokenAsync(requestModel);

            //if (response != null)
            //{
            //    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", response.AuthToken);
            //    return true;
            //}

            return false;
        }

        public async Task<string> GetTokenAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
        }

        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
        }
    }
}