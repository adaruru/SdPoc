using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProxyHost.Middleware
{
    public class ApRouterMiddleware
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly RequestDelegate _nextMiddleware;

        public ApRouterMiddleware(RequestDelegate nextMiddleware)
        {
            _nextMiddleware = nextMiddleware;
        }

        public async Task Invoke(HttpContext context)
        {
            await _nextMiddleware(context);
        }

    }
}
