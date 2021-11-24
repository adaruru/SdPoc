using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProxyHost.Middleware
{
    public class TestMiddleware
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly RequestDelegate _nextMiddleware;

        public TestMiddleware(RequestDelegate nextMiddleware)
        {
            _nextMiddleware = nextMiddleware;
        }

        public async Task Invoke(HttpContext context)
        {
            await _nextMiddleware(context);
        }

    }
}
