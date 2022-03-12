using WebService.Model;

namespace PerformanceApi.Lib
{
    public interface IPerformanceApiService
    {
        object GetPerformance();
        PerformanceSetting GetPerformanceSetting();
    }
}