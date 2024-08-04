namespace MedUnify.HealthPulseBlazor
{
    using MedUnify.HealthPulseBlazor;
    using MedUnify.HealthPulseBlazor.Services;
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
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7175/") });

            builder.Services.AddScoped<ApiService>();
            builder.Services.AddScoped<AuthenticationService>();

            await builder.Build().RunAsync();
        }
    }
}