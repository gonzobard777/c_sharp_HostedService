using Microsoft.Extensions.Hosting;

namespace ConsoleApp;

public class MyHostedService : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("MyHostedService.StartAsync");
        /*
         * Если раскомментить код ниже, то тогда приложение на самом деле не стартанет.
         * А при завершении будет выброшена ошибка:
         *     Unhandled exception. System.Threading.Tasks.TaskCanceledException: A task was canceled.
         * 
         */
        // while (!cancellationToken.IsCancellationRequested)
        // {
        //     Console.WriteLine("is Alive");
        //     await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        // }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("MyHostedService.StopAsync");
    }
}