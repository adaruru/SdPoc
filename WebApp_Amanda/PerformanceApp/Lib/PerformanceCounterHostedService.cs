using Service;
using PerformanceApp.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace PerformanceApp.Lib
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
        public object GetPerformance()
        {
            return new
            {
                CpuUsage = _performanceCollector.GetCpuUsage(),
                MemoryUsage = _performanceCollector.GetMemoryUsage(),
                IIS = _performanceCollector.GetMemoryUsage("IIS"),
                Calculator = _performanceCollector.GetMemoryUsage("Calculator")
            };
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var type = _configuration["type"];
            Console.WriteLine($"my set command line arg \"type\" : {type}");

            timer = new Timer(callback, null, 0, _performanceSetting.PoolIntervalSecond * 1000);
            return Task.CompletedTask;
        }

        private void callback(object obj)
        {

            var cpuUsage = _performanceCollector.GetCpuUsage();
            var memoryUsage = _performanceCollector.GetMemoryUsage();

            var IsCpuHigh = cpuUsage > _performanceSetting.CpuWarningUsage;

            Console.WriteLine(_configuration["Message"]);

            Console.Write($"cpu : {cpuUsage}%, memory : {memoryUsage}%");

            foreach (var app in _performanceSetting.AppName)
            {
                var appUsage = _performanceCollector.GetMemoryUsage(app);

                var IsAppCpuHigh = appUsage > _performanceSetting.CpuWarningUsage;
                var WarningMsq = IsAppCpuHigh ? "is High Usage" : string.Empty;

                Console.Write($", {app} : {appUsage}%" + WarningMsq);
            }
            Console.WriteLine(string.Empty);

            if (IsCpuHigh)
            {
                Console.Write($"MailTitle: { _notifySetting.MailTitle}% content:{ _notifySetting.MailContent} mail warning sent");
                Console.Write($"Usage Should not over: { _performanceSetting.CpuWarningUsage}% ");
            }
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
