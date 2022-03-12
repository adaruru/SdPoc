using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerformanceApi.Middleware
{
    public class ContextMiddleware
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly RequestDelegate _nextMiddleware;

        public ContextMiddleware(RequestDelegate nextMiddleware)
        {
            _nextMiddleware = nextMiddleware;
        }

        public async Task Invoke(HttpContext context)
        {
            //handle request 
            //await context.Response.WriteAsync("ContextMiddleware in. \r\n"); 
            await _nextMiddleware(context);
            await ErrorEventHandler(context);//作業5
        }

        private async Task ErrorEventHandler(HttpContext context)
        {
            if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
            {
                string originalPath = context.Request.Path.Value ?? "no path";
                context.Items["originalPath"] = originalPath;
                context.Response.WriteAsync($"該服務尚未開啟Path :{ originalPath}");
            }
        }
    }
}
