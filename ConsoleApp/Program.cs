using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsoleApp;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder()
                .ConfigureLogging((context, builder) => builder.AddConsole())
                .ConfigureServices(services =>
                {
                    // services.AddHostedService<MyHostedService>();
                    services.AddHostedService<MyBackgroundService>();
                    // services.AddHostedService<WriterWorker>();
                    // services.AddHostedService<ReaderWorker>();
                })
            ;
        await builder.Build().RunAsync();
    }
}