using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApp.Middleware
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
            //handle responce
            //await context.Response.WriteAsync("ContextMiddleware out. \r\n");
        }
        public async Task<HttpResponseMessage> InvokeAsync(HttpContext context) {
            //handle request 
            await context.Response.WriteAsync("ContextMiddleware in. \r\n");

            await _nextMiddleware(context);
            //handle responce
            await context.Response.WriteAsync("ContextMiddleware out. \r\n");
        }
    }
}
