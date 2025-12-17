using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Http;

namespace VEIVendorDemo
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    var apiSection = context.Configuration.GetSection("ApiSettings");
                    services.Configure<ApiSettings>(apiSection);

                    var settings = apiSection.Get<ApiSettings>();

                    services.AddHttpClient<ICallApiService, CallApiService>(client =>
                    {
                        client.BaseAddress = new Uri(settings.BaseUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                        client.DefaultRequestHeaders.Remove("Date");
                    }).ConfigurePrimaryHttpMessageHandler(() =>
                    {
                        return new HttpClientHandler
                        {
                            SslProtocols = System.Security.Authentication.SslProtocols.Tls12
                        };
                    });

                    services.AddTransient<Form1>();
                })
                .Build();

            using var scope = host.Services.CreateScope();
            var form = scope.ServiceProvider.GetRequiredService<Form1>();
            Application.Run(form);
        }
    }

    public class ApiSettings
    {
        public string BaseUrl { get; set; }
    }

    public interface ICallApiService
    {
        Task<string> SendStockInfoAsync();
    }

    public class CallApiService : ICallApiService
    {
        public Task<string> SendStockInfoAsync()
        {
            throw new NotImplementedException();
        }
    }


}