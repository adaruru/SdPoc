using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceApp.Lib;

public interface IScopedPerformanceCounterHostedService
{
    Task DoWorkAsync(CancellationToken stoppingToken);
}

