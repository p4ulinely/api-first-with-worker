using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Service;

public class MeuWorker : IHostedService
{
    private Timer _timer;
    private readonly string _conectionString;

    public MeuWorker(IConfiguration configuration)
    {
        _conectionString = configuration.GetConnectionString("SqlPrincipal");
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("MeuWorker rodou...");
        Console.WriteLine($"Conectando ao sql: {_conectionString}");

        _timer = new Timer(ExecutaAlgo, null, TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(1));

        return Task.CompletedTask;
    }

    private void ExecutaAlgo(object state)
    {
        System.Console.WriteLine($"Pulling -> {DateTime.Now}");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("MeuWorker finalizou...");

        return Task.CompletedTask;
    }
}
