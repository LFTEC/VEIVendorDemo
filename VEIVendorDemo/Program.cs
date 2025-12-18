using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Http;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using System.Text.Json.Nodes;

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

                    var mqSection = context.Configuration.GetSection("RabbitMQ");
                    services.Configure<RabbitMQSettings>(mqSection);

                    var settings = apiSection.Get<ApiSettings>();

                    services.AddHttpClient<ICallApiService, CallApiService>(client =>
                    {
                        client.BaseAddress = new Uri(settings.BaseUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                        client.DefaultRequestHeaders.Remove("Date");
                        client.DefaultRequestHeaders.Add("X-Ca-Stage", "PRE");
                        client.DefaultRequestHeaders.Add("X-Ca-Signature-Headers", "X-Ca-Stage");
                        client.DefaultRequestHeaders.Add("x-ca-key", settings.ApiKey);
                        client.DefaultRequestHeaders.Add("x-ca-signature-method", "HmacSHA256");
                    }).ConfigurePrimaryHttpMessageHandler(() =>
                    {
                        return new HttpClientHandler
                        {
                            SslProtocols = System.Security.Authentication.SslProtocols.Tls12
                        };
                    });

                    services.AddSingleton<RabbitMQConnector>();
                    services.AddSingleton<IRabbitMQConnector>(sp=>sp.GetRequiredService<RabbitMQConnector>());
                    services.AddHostedService<RabbitMQConnector>(sp=>sp.GetRequiredService<RabbitMQConnector>());

                    services.AddTransient<Form1>();
                })
                .Build();

            host.Start();
            using var scope = host.Services.CreateScope();
            var form = scope.ServiceProvider.GetRequiredService<Form1>();
            Application.Run(form);

            host.StopAsync().GetAwaiter().GetResult();
        }
    }

    public class ApiSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string ApiSecret { get; set; } = string.Empty;
    }

    public class RabbitMQSettings
    {
        public string? HostName { get; set; }
        public string? VirtualHost { get; set; }
        public int Port { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    public interface ICallApiService
    {
        Task<string> SendStockInfoAsync(StockMoveData data);
    }

    public class CallApiService : ICallApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;

        public CallApiService(HttpClient httpClient, IOptions<ApiSettings> options)
        {
            _httpClient = httpClient;
            _apiSettings = options.Value;
        }
        public async Task<string> SendStockInfoAsync(StockMoveData data)
        {
            var body = new { type = "API_VEI_STOCK_MOVE", timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), data = data };
            string message = System.Text.Json.JsonSerializer.Serialize(body, new System.Text.Json.JsonSerializerOptions
            {
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            var content = new StringContent(message, System.Text.Encoding.UTF8, "application/json");

            string path = "/MM/01622/purapi";
            string method = "POST";
            string accept = "application/json";
            string contentMd5 = "";// Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(message)));
            string contentType = "application/json; charset=utf-8";
            string header = "X-Ca-Stage:PRE";

            StringBuilder sb = new StringBuilder();
            sb.AppendJoin("\n",
                method,
                accept,
                contentMd5,
                contentType,
                "", // Date is empty
                header,
                path
            );

            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_apiSettings.ApiSecret));
            var signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())));
            var request = new HttpRequestMessage(HttpMethod.Post, path);
            request.Headers.Add("X-Ca-Signature", signature);
            request.Headers.Remove("Date");
            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            if(response.IsSuccessStatusCode)
            {
                var ret = await response.Content.ReadAsStringAsync();
                var resultNode = JsonNode.Parse(ret);
                if (resultNode != null)
                {
                    var state = resultNode["state"]?.GetValue<string>();
                    if (state == "E")
                        return resultNode["msgText"]?.GetValue<string>() ?? "Unknown error";
                }
                else
                {
                    return "Invalid response from API";
                }
            }
            else
            {
                throw new Exception($"API request failed with status code: {response.StatusCode}, message: {await response.Content.ReadAsStringAsync()}");
            }

            return "";
        }
    }


}