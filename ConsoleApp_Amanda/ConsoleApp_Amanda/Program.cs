using Common;
using ConsoleApp_Amanda.Lib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Host.CreateDefaultBuilder(args)
    .ConfigureServices(service => service
        //add service 沒有先後順序
        .AddSingleton<IHostedService, PerformanceCounterHostedService>()
        .AddSingleton<IPerformanceCollector, PerformanceCollector>())
    .Build()
    .Run();





