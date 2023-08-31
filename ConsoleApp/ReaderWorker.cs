using Microsoft.Extensions.Hosting;
using static System.Console;
using System.Diagnostics;

namespace ConsoleApp;

public class ReaderWorker : BackgroundService
{
    public ReaderWorker(IHostApplicationLifetime hostApplicationLifetime)
    {
        hostApplicationLifetime.ApplicationStarted.Register(() => WriteLine($"In ReaderWorker - host application started at: {DateTimeOffset.Now}."));
        hostApplicationLifetime.ApplicationStopping.Register(() => WriteLine($"In ReaderWorker - host application stopping at: {DateTimeOffset.Now}."));
        hostApplicationLifetime.ApplicationStopped.Register(() => WriteLine($"In ReaderWorker - host application stopped at: {DateTimeOffset.Now}."));
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        WriteLine($"ReaderWorker started at: {DateTimeOffset.Now}");
        return base.StartAsync(cancellationToken);
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.Register(() => WriteLine($"In ReaderWorker - token was cancelled at: {DateTimeOffset.Now}."));
        while (!stoppingToken.IsCancellationRequested)
        {
            WriteLine($"ReaderWorker running at: {DateTimeOffset.Now}");
            await Task.Delay(TimeSpan.FromSeconds(120));
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        var stopWatch = Stopwatch.StartNew();
        WriteLine($"ReaderWorker stopped at: {DateTimeOffset.Now}");
        await base.StopAsync(cancellationToken);
        WriteLine($"ReaderWorker took {stopWatch.ElapsedMilliseconds} ms to stop.");
    }

}