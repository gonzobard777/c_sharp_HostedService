using Microsoft.Extensions.Hosting;
using static System.Console;
using System.Diagnostics;

namespace ConsoleApp;

public class WriterWorker : BackgroundService
{
    public WriterWorker(IHostApplicationLifetime hostApplicationLifetime)
    {
        hostApplicationLifetime.ApplicationStarted.Register(() => WriteLine($"In WriterWorker - host application started at: {DateTimeOffset.Now}."));
        hostApplicationLifetime.ApplicationStopping.Register(() => WriteLine($"In WriterWorker - host application stopping at: {DateTimeOffset.Now}."));
        hostApplicationLifetime.ApplicationStopped.Register(() => WriteLine($"In WriterWorker - host application stopped at: {DateTimeOffset.Now}."));
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        WriteLine($"WriterWorker started at: {DateTimeOffset.Now} and will take 5 seconds to complete.");
        await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.Register(() => WriteLine($"In WriterWorker - token was cancelled at: {DateTimeOffset.Now}."));
        while (!stoppingToken.IsCancellationRequested)
        {
            WriteLine($"WriterWorker running at: {DateTimeOffset.Now}");
            //If you pass cancellation token here or to your work task
            //then tasks will be completed and you will not observe extended shutdown time.
            //try this same code but pass cancellation token
            //await Task.Delay(TimeSpan.FromSeconds(120), stoppingToken); 
            await Task.Delay(TimeSpan.FromSeconds(120));
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        var stopWatch = Stopwatch.StartNew();
        WriteLine($"WriterWorker stopped at: {DateTimeOffset.Now}");
        await base.StopAsync(cancellationToken);
        WriteLine($"WriterWorker took {stopWatch.ElapsedMilliseconds} ms to stop.");
    }
}