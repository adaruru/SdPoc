using Flurl;
using System.Text;
using Microsoft.AspNetCore.Http.Extensions;

namespace OldTemplate.Middleware
{
    //參考:https://exceptionnotfound.net/using-middleware-to-log-requests-and-responses-in-asp-net-core/
    public class HttpLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpLogMiddleware> _logger;

        public HttpLogMiddleware(RequestDelegate next, ILogger<HttpLogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            //First, get the incoming request
            var request = await FormatRequest(context.Request);
            _logger.LogInformation("ProxyMiddleware request : {0}", request);

            //Copy a pointer to the original response body stream
            Stream originalBodyStream = context.Response.Body;

            //Create a new memory stream...
            using (var responseBody = new MemoryStream())
            {
                //...and use that for the temporary response body
                context.Response.Body = responseBody;

                //Continue down the Middleware pipeline, eventually returning to this class
                await _next(context);

                //Format the response from the server
                var response = await FormatResponse(context.Response);

                //Save log to chosen datastore
                _logger.LogInformation("ProxyMiddleware response : {0}", response);

                //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;

            //This line allows us to set the reader for the request back at the beginning of its stream.
            request.EnableBuffering();

            //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            //...Then we copy the entire request stream into the new buffer.
            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            //We convert the byte[] into a string using UTF8 encoding...
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            //..and finally, assign the read body back to the request body, which is allowed because of EnableRewind()

            foreach (var header in request.Headers)
            {
                _logger.LogInformation("ProxyMiddleware FormatRequest header , {0}{1}", header.Key.PadRight(25, '_'), header.Value);
            }

            var uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Port = request.Host.Port.GetValueOrDefault(80),
                Path = request.Path.ToString(),
                Query = request.QueryString.ToString()
            };

            _logger.LogInformation("ProxyMiddleware FormatRequest requestUrl ,{0}", UriHelper.GetEncodedUrl(request));

            return uriBuilder.ToString();
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            var result = $"{response.StatusCode}: {text}";
            return result;
        }
    }
}
