using Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp_Amanda.Lib
{
    public class PerformanceCounterHostedService : IHostedService
    {

        private Timer timer;

        private IConfiguration Configuration;
        private IPerformanceCollector PerformanceCollector;
        public PerformanceCounterHostedService(IConfiguration configuration, IPerformanceCollector performanceCollector)
        {
            Configuration = configuration;
            PerformanceCollector = performanceCollector;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var type = Configuration["type"];
            Console.WriteLine($"my set command line arg \"type\" : {type}");

            timer = new Timer(callback, null, 0, 1000);
            return Task.CompletedTask;
        }

        private void callback(object obj)
        {
            var cpuUsage = PerformanceCollector.GetCpuUsage();
            var memoryUsage = PerformanceCollector.GetMemoryUsage();
            var caculator = PerformanceCollector.GetMemoryUsage("caculator");
            Console.WriteLine($"cpu : {cpuUsage}%, memory : {memoryUsage}%, caculator : {caculator}%");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Change(-1, 0);
            timer?.Dispose();
            Console.ReadKey();
            return Task.CompletedTask;
        }
    }
}
