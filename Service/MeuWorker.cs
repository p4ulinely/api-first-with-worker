using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Service;

public class MeuWorker : IHostedService
{
    private Timer _timer;
    private readonly string _conectionString;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<MeuWorker> _logger;

    public MeuWorker(
        ILogger<MeuWorker> logger, IHttpClientFactory httpClient, IConfiguration configuration)
    {
        _logger = logger;
        _httpClientFactory = httpClient;
        // _conectionString = configuration.GetConnectionString("SqlPrincipal");
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("MeuWorker rodou...");
        Console.WriteLine($"Conectando ao sql: {_conectionString}");

        _timer = new Timer(ExecutaAlgo, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }

    private void ExecutaAlgo(object state)
    {
        try
        {
            Console.WriteLine($"Pulling data from some webservice -> {DateTime.Now}");

            var httpClient = _httpClientFactory.CreateClient("ApiVagas");
            var response = httpClient.GetAsync("vagas/api/v1/issues").Result;

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                _logger.LogInformation(response.StatusCode.ToString());
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("MeuWorker finalizou...");

        return Task.CompletedTask;
    }
}
