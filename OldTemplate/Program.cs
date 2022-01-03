using AspNetCore.Proxy;
using OldTemplate.Lib.Biz;
using OldTemplate.Lib.Common;
using OldTemplate.Middleware;
using OldTemplate.Model;
using NLog;
using NLog.Web;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

var logger = LogManager.Setup()
                       .LoadConfigurationFromAppSettings()
                       .GetCurrentClassLogger();
logger.Debug("init ProxyService");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();

    //lib access context
    //builder.Services.AddHttpContextAccessor();

    //NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(LogLevel.Debug);
    builder.Host.UseNLog();

    builder.Services.AddMemoryCache();

    //todo:delete local config
    builder.Services.Configure<ProxySetting>(options => builder.Configuration.GetSection("ProxySetting").Bind(options));

    builder.Services.AddProxies();

    builder.Services.AddLibs();
    builder.Services.AddClientHandler();


    var app = builder.Build();

    //Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    //Todo: question http logging not working
    app.UseHttpLogging();

    app.UseHttpsRedirection();
    app.UseRouting();

    app.UseMiddleware<CustomHeaderMiddleware>();
    app.UseMiddleware<HttpLogMiddleware>();

    app.RunProxy(proxy => proxy
        .UseHttp((context, args) =>
        {
            var proxyLib = context.RequestServices.GetRequiredService<IProxyBiz>();
            var endpoint = proxyLib.ProxyGetEndpoint(context);
            return endpoint;
        }, builder => builder.WithHttpClientName("ProxyClientBuilder"))
    );

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
