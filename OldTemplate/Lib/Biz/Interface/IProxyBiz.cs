using OldTemplate.Model;

namespace OldTemplate.Lib.Biz
{
    public interface IProxyBiz
    {
        T? GetHeader<T>(HttpContext context) where T : new();
        ProxySetting GetProxySetting();
        string ProxyGetEndpoint(HttpContext context);
        void SetNavRoutes(ProxySetting proxy);
        void SetNavRoutesHostInfos();
    }
}