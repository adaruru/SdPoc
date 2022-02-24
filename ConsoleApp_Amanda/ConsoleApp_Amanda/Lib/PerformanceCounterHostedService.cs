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

            timer = new Timer(callback, null, 0, 1000);
            return Task.CompletedTask;
        }

        private void callback(object obj)
        {

            var cpuUsage = _performanceCollector.GetCpuUsage();
            var memoryUsage = _performanceCollector.GetMemoryUsage();
            var caculator = _performanceCollector.GetMemoryUsage("caculator");
            Console.WriteLine($"cpu : {cpuUsage}%, memory : {memoryUsage}%, caculator : {caculator}%");
            Console.WriteLine($"_performanceSetting : {_performanceSetting.AppName[0]}");
            Console.WriteLine($"_exitSetting : {_notifySetting.MailTitle}");
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
