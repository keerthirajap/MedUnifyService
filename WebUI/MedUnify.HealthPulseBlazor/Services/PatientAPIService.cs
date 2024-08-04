namespace MedUnify.HealthPulseBlazor.Services
{
    using MedUnify.HealthPulseBlazor.Pages.Patients;
    using MedUnify.HealthPulseBlazor.Providers;
    using MedUnify.ResourceModel;
    using MedUnify.ResourceModel.HealthPulse;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Text.Json;

    public class PatientAPIService
    {
        private readonly TokenAuthStateProvider _tokenAuthStateProvider;

        private readonly HttpClient _httpClient;

        public PatientAPIService(HttpClient httpClient, TokenAuthStateProvider tokenAuthStateProvider)
        {
            _tokenAuthStateProvider = tokenAuthStateProvider;
            _httpClient = httpClient;
        }

        private async Task AddAuthorizationHeaderAsync()
        {
            // Assume you have a method to retrieve the token
            string token = await _tokenAuthStateProvider.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<List<PatientRM>> GetPatientsAsync()
        {
            await AddAuthorizationHeaderAsync();
            return await _httpClient.GetFromJsonAsync<List<PatientRM>>("Patients/GetPatients");
        }

        public async Task<PatientRM> GetPatientByIdAsync(int patientId)
        {
            await AddAuthorizationHeaderAsync();
            return await _httpClient.GetFromJsonAsync<PatientRM>($"Patients/GetPatient?patientId={patientId}");
        }

        public async Task<HttpResponseMessage> UpdatePatientAsync(int patientId, PatientRM patient)
        {
            await AddAuthorizationHeaderAsync();
            var response = await _httpClient.PutAsJsonAsync($"Patients/UpdatePatient?patientId={patientId}", patient);

            return response;
        }

        //public async Task AddPatientAsync(Patient patient)
        //{
        //    await AddAuthorizationHeaderAsync();
        //    var response = await _httpClient.PostAsJsonAsync("https://api.example.com/AddPatient", patient);
        //    response.EnsureSuccessStatusCode();
        //}

        //public async Task DeletePatientAsync(int patientId)
        //{
        //    await AddAuthorizationHeaderAsync();
        //    var response = await _httpClient.DeleteAsync($"https://api.example.com/DeletePatient?id={patientId}");
        //    response.EnsureSuccessStatusCode();
        //}

        private async Task<string> GetTokenAsync()
        {
            // Implement your logic to retrieve the token here
            return await Task.FromResult("your-authentication-token");
        }
    }
}