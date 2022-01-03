using OldTemplate.Model;
using Microsoft.AspNetCore.Http.Headers;
using Newtonsoft.Json;

namespace OldTemplate.Lib.Common
{
    public class HttpContextExtensions
    {
        /// <summary>
        /// 自定義 ClientHandler
        /// bypass 沒有 ssl 的request
        /// </summary>
        /// <returns></returns>
        public static HttpClientHandler CustomClientHandler()
        {
            var client = new HttpClientHandler()
            {
                // Credentials are necessary if the server requires the client 
                // to authenticate before it will send email on the client's behalf.
                UseDefaultCredentials = true,

                //HttpClientFactory  bypass none ssl request
                //1. delegate { return true; },
                //2. (sender, cert, chain, sslPolicyErrors) => { return true; },
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            return client;
        }
    }
}
