using OldTemplate.Lib.Biz;
using Microsoft.AspNetCore.HttpLogging;
using NLog;
using NLog.Web;

namespace OldTemplate.Lib.Common
{
    public static class DIHelper
    {
        public static IServiceCollection AddLibs(this IServiceCollection services)
        {
            services.AddSingleton<IProxyBiz, ProxyBiz>();
            services.AddSingleton<IConfigHelper, ConfigHelper>();
            //services.AddSingleton<ICustomHeaderPolicyFactory, CustomHeaderPolicy>();
            services.AddSingleton<IRobinHelper, RobinHelper>();
            services.AddSingleton<ILogHelper, LogHelper>();
            return services;
        }

        public static IServiceCollection AddClientHandler(this IServiceCollection services)
        {
            services.AddHttpClient("ProxyClientBuilder")
                    .ConfigurePrimaryHttpMessageHandler(() => HttpContextExtensions.CustomClientHandler());

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
    }
}