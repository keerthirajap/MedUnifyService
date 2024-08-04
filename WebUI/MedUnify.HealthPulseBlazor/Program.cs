namespace MedUnify.HealthPulseBlazor
{
    using MedUnify.HealthPulseBlazor;
    using MedUnify.HealthPulseBlazor.Providers;
    using MedUnify.HealthPulseBlazor.Services;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Configure the HttpClient for the Blazor WebAssembly (main app URL)
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // Configure the HttpClient for the API service with a different base URL
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7175/") });

            //builder.Services.AddScoped(sp =>
            //{
            //    var client = new HttpClient { BaseAddress = new Uri("https://api.example.com/") };
            //    return new PatientAPIService(client);
            //});

            builder.Services.AddScoped<AuthAPIService>();

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<TokenAuthStateProvider>();

            builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<TokenAuthStateProvider>());

            builder.Services.AddHttpClient<AuthAPIService>((sp, client) =>
            {
                client.Timeout = TimeSpan.FromSeconds(Convert.ToInt32(30));

                client.BaseAddress = new Uri("https://localhost:7175/api/");

                //client.Timeout = TimeSpan.FromSeconds(Convert.ToInt32(builder.Configuration["API_URLS:GettHit.TraficEx.Timeout"]));

                //client.BaseAddress = new Uri(builder.Configuration["API_URLS:GettHit.TraficEx"]);
                //client.EnableIntercept(sp);
            });

            builder.Services.AddHttpClient<PatientAPIService>((sp, client) =>
            {
                client.Timeout = TimeSpan.FromSeconds(Convert.ToInt32(30));

                client.BaseAddress = new Uri("https://localhost:7120/api/");

                //client.Timeout = TimeSpan.FromSeconds(Convert.ToInt32(builder.Configuration["API_URLS:GettHit.TraficEx.Timeout"]));

                //client.BaseAddress = new Uri(builder.Configuration["API_URLS:GettHit.TraficEx"]);
                //client.EnableIntercept(sp);
            });

            await builder.Build().RunAsync();
        }
    }
}