using Microsoft.Extensions.Hosting;

namespace ConsoleApp;

public class MyBackgroundService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("MyBackgroundService.ExecuteAsync");
        while (!cancellationToken.IsCancellationRequested)
        {
            Console.WriteLine("is Alive");
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        }
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("MyBackgroundService.StartAsync");
        await base.StartAsync(cancellationToken);
    }


    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("MyBackgroundService.StopAsync");
        await base.StopAsync(cancellationToken);
    }
}