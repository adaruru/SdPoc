namespace Service
{
    public interface IPerformanceCollector
    {
        int GetCpuUsage();
        int GetMemoryUsage();
        int GetMemoryUsage(string processName);
    }
}