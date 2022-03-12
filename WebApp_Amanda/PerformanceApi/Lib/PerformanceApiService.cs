using Microsoft.Extensions.Options;
using WebService;
using WebService.Model;

namespace PerformanceApi.Lib;

public class PerformanceApiService : IPerformanceApiService
{
    private readonly IConfiguration _configuration;
    private readonly IPerformanceCollector _performanceCollector;

    public readonly PerformanceSetting _performanceSetting;
    public readonly NotifySetting _notifySetting;

    public PerformanceApiService(IConfiguration configuration, IPerformanceCollector performanceCollector, IOptions<PerformanceSetting> performanceSettingOption, IOptions<NotifySetting> notifySettingOption)
    {
        _configuration = configuration;
        _performanceCollector = performanceCollector;
        _performanceSetting = performanceSettingOption.Value;
        _notifySetting = notifySettingOption.Value;
    }

    public PerformanceSetting GetPerformanceSetting()
    {
        return _performanceSetting;
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
}
