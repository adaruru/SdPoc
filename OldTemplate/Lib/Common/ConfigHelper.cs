using OldTemplate.Model;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace OldTemplate.Lib.Common
{
    public class ConfigHelper : IConfigHelper
    {
        public string Container { get; set; }
        private readonly IMemoryCache _cache;
        private readonly ILogger<ConfigHelper> _logger;

        public ConfigHelper(IMemoryCache cache, ILogger<ConfigHelper> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        /// <summary>
        /// 設定檔存 10 天，當設定檔有更新應清空快取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void SetDataToCache(string key, byte[] data)
        {
            try
            {
                if (data != null)
                {
                    var cacheEntryOptions =
                        new MemoryCacheEntryOptions()
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),//保存時間
                        }.SetSlidingExpiration(TimeSpan.FromMinutes(5)); //存取時刷新保存時間FromSeconds(60)

                    _logger.LogDebug("SetDataToCache :{0}", key);

                    _cache.Set(key, data, cacheEntryOptions);
                }
                else
                {
                    _logger.LogError("key exist {0}, cache not found and no setting Data", key);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("SetDataToCache fail ex:{0}", ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public T GetConfig<T>(FileArg fileArg)
        {
            try
            {
                _logger.LogDebug("GetConfig");

                byte[] cachedValue;
                var key = fileArg.Key;
                if (string.IsNullOrWhiteSpace(fileArg.Key))
                {
                    key = typeof(T).Name;
                }

                var cacheSuccess = _cache.TryGetValue(key, out cachedValue);

                _logger.LogDebug("cacheSuccess: {0}", cacheSuccess);

                if (!cacheSuccess)
                {
                    //todo wait FileService SDK
                    var fileBytes = MockFileProvider(fileArg);
                    SetDataToCache(key, fileBytes);

                    var result = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(fileBytes));

                    _logger.LogDebug("GetConfig From FileService and SetDataToCache {0}", JsonConvert.SerializeObject(result));

                    return result;
                }
                else
                {
                    var result = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(cachedValue));
                    _logger.LogDebug("GetConfig From Cache :{0}", JsonConvert.SerializeObject(result));
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GetConfig fail ex:{0}", ex.ToString());
                throw;
            }
        }

        //todo wait FileService SDK delete
        /// <summary>
        /// </summary>
        /// <param name="fileArg"></param>
        /// <returns></returns>
        public byte[] MockFileProvider(FileArg fileArg)
        {
            var url = string.Empty;
            _logger.LogDebug("fileArg : {0}", JsonConvert.SerializeObject(fileArg));

            if (fileArg.Container == "ITSProxyService" && fileArg.FilePath == "Config/ProxySettings.json")
            {
                _logger.LogDebug("MockFileProvider get ITSProxyService");
                url = "https://localhost:12443/File/GetProxySetting";
            }
            else if (fileArg.Container == "ITSProxyService" && fileArg.FilePath == "Config/GetServerHostsSetting.json")
            {
                _logger.LogDebug("MockFileProvider get ITSProxyService");
                url = "https://localhost:12443/File/GetServerHostsSetting";
            }

            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                //Todo Itslib HttpWebRequest 
                request.Method = "get";
                request.Headers.Add("access_key", "your access_key");
                request.Accept = "application/json";
                request.ContentType = "application/json; charset=utf-8";

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        using (var memstream = new MemoryStream())
                        {
                            reader.BaseStream.CopyTo(memstream);
                            return memstream.ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
