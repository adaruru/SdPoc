using OldTemplate.Lib.Common;
using static OldTemplate.Lib.Utils.CustomHeaderPolicy;

namespace OldTemplate.Middleware
{
    public class CustomHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ICustomHeaderPolicyFactory _policy=;

        public CustomHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //參考:https://andrewlock.net/adding-default-security-headers-in-asp-net-core/
        public async Task Invoke(HttpContext context)
        {
            IHeaderDictionary headers = context.Request.Headers;

            //header add client ip (測試clent ip 修改)         
            headers.Add("ClientIP", context.Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString()??"IpRecordFail");

            foreach (var headerValuePair in SetHeaders)
            {
                headers[headerValuePair.Key] = headerValuePair.Value;
            }

            foreach (var header in AddHeaders)
            {
                headers.Add(header.Key, header.Value);
            }

            foreach (var header in RemoveHeaders)
            {
                headers.Remove(header);
            }

            await _next(context);
            context.Response.Headers.Add("InvokeWait", "ResponseAdd");
        }

    }
}
