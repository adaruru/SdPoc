using Microsoft.Extensions.Options;
using Service;
using PerformanceApp.Model;
using PerformanceApp.Lib;

namespace PerformanceApi.Lib;

public class PerformanceApiService : IPerformanceApiService
{
    private readonly IConfiguration _configuration;
    private readonly IPerformanceCollector _performanceCollector;

    public readonly PerformanceSetting _performanceSetting;
    public readonly NotifySetting _notifySetting;

    private readonly PerformanceCounterHostedService _performanceCounterHostedService;

    public PerformanceApiService(
        IConfiguration configuration,
        IPerformanceCollector performanceCollector,
        IOptions<PerformanceSetting> performanceSettingOption,
        IOptions<NotifySetting> notifySettingOption,
        PerformanceCounterHostedService performanceCounterHostedService)
    {
        _configuration = configuration;
        _performanceCollector = performanceCollector;
        _performanceSetting = performanceSettingOption.Value;
        _notifySetting = notifySettingOption.Value;
        _performanceCounterHostedService = performanceCounterHostedService;
    }

    public PerformanceSetting GetPerformanceSetting()
    {
        return _performanceSetting;
    }

    public object GetPerformanceFromService()
    {
        return new
        {
            CpuUsage = _performanceCollector.GetCpuUsage(),
            MemoryUsage = _performanceCollector.GetMemoryUsage(),
            IIS = _performanceCollector.GetMemoryUsage("IIS"),
            Calculator = _performanceCollector.GetMemoryUsage("Calculator")
        };
    }
    public object GetPerformanceFromConsole()
    {
        return _performanceCounterHostedService.GetPerformance();
    }
}
