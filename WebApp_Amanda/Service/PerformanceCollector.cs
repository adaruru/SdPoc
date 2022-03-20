namespace Service
{
    public class PerformanceCollector : IPerformanceCollector
    {
        public int GetCpuUsage()
        {
            return new Random().Next(0, 100);
        }
        public int GetMemoryUsage()
        {
            return new Random().Next(0, 99999);
        }
        public int GetMemoryUsage(string processName)
        {
            return new Random().Next(0, 99999);
        }
    }
}