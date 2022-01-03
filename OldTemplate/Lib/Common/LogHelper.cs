using Flurl;
using Microsoft.AspNetCore.Http.Headers;
using Newtonsoft.Json;

namespace OldTemplate.Lib.Common
{
    public class LogHelper : ILogHelper
    {
        private readonly ILogger<LogHelper> _logger;

        public LogHelper(ILogger<LogHelper> logger)
        {
            _logger = logger;
        }
        
        public void UrlDebugLogging(Uri uri)
        {
            _logger.LogDebug($"url Detail:{uri.AbsoluteUri}");
        }

        public void ObjectDebugLogging<T>(T data, string dataName = null)
        {
            if (dataName == null)
            {
                dataName = typeof(T).Name;
            }
            _logger.LogDebug(dataName + " : {0}", JsonConvert.SerializeObject(data));
        }
        public void ObjectErrorLogging<T>(T data, string dataName = null)
        {
            if (dataName == null)
            {
                dataName = typeof(T).Name;
            }
            _logger.LogError(dataName + " : {0}", JsonConvert.SerializeObject(data));
        }
    }
}
