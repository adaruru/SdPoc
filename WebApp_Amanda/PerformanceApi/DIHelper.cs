using PerformanceApi.Lib;
using System.Configuration;
using Service;
using PerformanceApp.Model;

namespace PerformanceApi;

public static class DIHelper
{
    public static IServiceCollection AddLibs(this IServiceCollection services)
    {
        services.AddScoped<IPerformanceCollector, PerformanceCollector>();
        services.AddScoped<IPerformanceApiService, PerformanceApiService>();
        return services;
    }

    public static IServiceCollection AddClientHandler(this IServiceCollection services)
    {
        services.AddHttpClient("bypassSsl")
                .ConfigurePrimaryHttpMessageHandler(() => CustomClientHandler());

        //Todo: question not working http logging
        //services.AddHttpLogging(logging =>
        //{
        //    // Customize HTTP logging here.
        //    logging.LoggingFields = HttpLoggingFields.All;
        //    logging.RequestHeaders.Add("Request-Header-Demo");
        //    logging.ResponseHeaders.Add("Response-Header-Demo");
        //    logging.MediaTypeOptions.AddText("application/javascript");
        //    logging.RequestBodyLogLimit = 4096;
        //    logging.ResponseBodyLogLimit = 4096;
        //});

        return services;
    }

    /// <summary>
    /// 自定義 ClientHandler
    /// bypass 沒有 ssl 的request
    /// </summary>
    /// <returns></returns>
    public static HttpClientHandler CustomClientHandler()
    {
        return new HttpClientHandler()
        {
            UseDefaultCredentials = true,
            //bypass 沒有 ssl 的request (亦可等於
            //1. delegate { return true; },
            //2. (sender, cert, chain, sslPolicyErrors) => { return true; },
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
    }

}
