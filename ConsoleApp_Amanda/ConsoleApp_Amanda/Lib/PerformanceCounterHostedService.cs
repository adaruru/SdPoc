using Common;
using ConsoleApp_Amanda.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace ConsoleApp_Amanda.Lib
{
    public class PerformanceCounterHostedService : IHostedService
    {

        private Timer timer;

        private readonly IConfiguration _configuration;

        private readonly IPerformanceCollector _performanceCollector;

        private readonly PerformanceSetting _performanceSetting;
        private readonly NotifySetting _notifySetting;

        public PerformanceCounterHostedService(IConfiguration configuration, IPerformanceCollector performanceCollector, IOptions<PerformanceSetting> performanceSettingOption, IOptions<NotifySetting> notifySettingOption)
        {
            _configuration = configuration;
            _performanceCollector = performanceCollector;
            _performanceSetting = performanceSettingOption.Value;
            _notifySetting = notifySettingOption.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var type = _configuration["type"];
            Console.WriteLine($"my set command line arg \"type\" : {type}");

            timer = new Timer(callback, null, 0, _performanceSetting.PoolIntervalSecond*1000);
            return Task.CompletedTask;
        }

        private void callback(object obj)
        {

            var cpuUsage = _performanceCollector.GetCpuUsage();
            var memoryUsage = _performanceCollector.GetMemoryUsage();

            Console.WriteLine(_configuration["Message"]);

            Console.Write($"cpu : {cpuUsage}%, memory : {memoryUsage}%");

            foreach (var app in _performanceSetting.AppName)
            {
                var appUsage = _performanceCollector.GetMemoryUsage(app);
                Console.Write($", {app} : {appUsage}%");
            }
            Console.WriteLine(string.Empty);
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
