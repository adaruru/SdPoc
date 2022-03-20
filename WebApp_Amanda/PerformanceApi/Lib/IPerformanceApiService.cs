using PerformanceApp.Model;

namespace PerformanceApi.Lib
{
    public interface IPerformanceApiService
    {
        object GetPerformanceFromConsole();
        object GetPerformanceFromService();
        PerformanceSetting GetPerformanceSetting();
    }
}