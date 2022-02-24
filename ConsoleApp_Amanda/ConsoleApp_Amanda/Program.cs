using Common;
using ConsoleApp_Amanda.Lib;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ConsoleApp_Amanda.Model;


var hostBuilder = Host.CreateDefaultBuilder(args);

hostBuilder.ConfigureAppConfiguration(hostConfig =>
{
   // hostConfig.SetBasePath(Directory.GetCurrentDirectory());
    hostConfig.AddJsonFile("notifysetting.json", optional: true, reloadOnChange: true);
    //hostConfig.AddEnvironmentVariables(prefix: "PREFIX_");
    //hostConfig.AddCommandLine(args);
});


hostBuilder.ConfigureServices((context, services) =>
{
    ConfigureServices(context.Configuration, services);
});

var host = hostBuilder.Build();
host.Run();



void ConfigureServices(IConfiguration configuration,
 IServiceCollection services)
{
    services.Configure<PerformanceSetting>(options => configuration.GetSection("PerformanceSetting").Bind(options));
    services.Configure<NotifySetting>(options => configuration.GetSection("NotifySetting").Bind(options));

    services.AddSingleton<IHostedService, PerformanceCounterHostedService>();
    services.AddSingleton<IPerformanceCollector, PerformanceCollector>();

}





