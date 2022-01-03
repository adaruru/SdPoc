using OldTemplate.Model;
using OldTemplate.Lib.Common;
using Newtonsoft.Json;


namespace OldTemplate.Lib.Biz
{
    public class ProxyBiz : IProxyBiz
    {
        private readonly IConfigHelper _configHelper;
        private readonly IRobinHelper _robinHelper;

        private readonly ILogHelper _logLib;
        private readonly ILogger<ProxyBiz> _logger;

        public ProxyBiz(IConfigHelper configHelper, IRobinHelper robinHelper, ILogHelper logLib, ILogger<ProxyBiz> logger)
        {
            _configHelper = configHelper;
            _robinHelper = robinHelper;
            _logLib = logLib;
            _logger = logger;
        }

        private CustomHeader customHeader;
        private NavigationList<RouteSetting> NavRoutes;

        public string ProxyGetEndpoint(HttpContext context)
        {
            try
            {
                if (context == null)
                {
                    return string.Empty;
                }

                customHeader = GetHeader<CustomHeader>(context) ?? new CustomHeader();
                var savedTarget = new Target();
                NavRoutes = new NavigationList<RouteSetting>();

                var proxySetting = GetProxySetting();
                SetNavRoutes(proxySetting);
                SetNavRoutesHostInfos();

                var route = NavRoutes.Current;
                var target = route.Target;
                target.CustomHeader = customHeader;
                target.Port = route.Port;
                return _robinHelper.GetEndpoint(target);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public T? GetHeader<T>(HttpContext context) where T : new()
        {
            try
            {
                T? result = new T();
                foreach (var prop in typeof(T).GetProperties())
                {
                    if (context.Request.Headers[prop.Name].Count() > 0)
                    {
                        prop.SetValue(result, context.Request.Headers[prop.Name][0]);
                    }
                }

                _logLib.ObjectDebugLogging(result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetHeader fail ex:{0}", ex.ToString());
                throw;
            }
        }

        public ProxySetting GetProxySetting()
        {
            try
            {
                _logger.LogDebug("GetProxySetting");
                var proxyFileArg = new FileArg()
                {
                    Container = "ITSProxyService",
                    FilePath = "Config/ProxySettings.json"
                };
                var result = _configHelper.GetConfig<ProxySetting>(proxyFileArg);

                if (result == null ||
                    result.RouteSettings == null ||
                    result.RouteSettings.Count() <= 0)
                {
                    _logger.LogError(@"ProxySetting Not valid or RouteSetting Not found");
                    //Todo return fail
                }
                _logLib.ObjectDebugLogging(result);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetProxySetting fail ex:{0}", ex.ToString());
                throw;
            }
        }

        public void SetNavRoutes(ProxySetting proxy)
        {
            try
            {
                var filteredRoute = proxy.RouteSettings
                                     .Where(r => r.Port != null &&
                                                 r.Service == customHeader.Service
                                                 )
                                     .OrderBy(r => r.Priority).ToList();

                if (filteredRoute.Count() <= 0)
                {
                    _logger.LogError("Service : {0} , RouteSetting Not found", customHeader.Service);
                    //Todo return fail
                }

                foreach (var route in filteredRoute)
                {
                    var validHosts = new List<HostSetting>();
                    if (route.Target == null ||
                        route.Target.HostInfos.Count <= 0 ||
                        route.Port == null)
                    {
                        _logger.LogError("Route[{0}] Not Valid: HostInfos List should not Empty", route.RouteSettingId);
                        _logLib.ObjectErrorLogging(route);
                        continue;
                        //Todo question only log no need fail
                    }

                    validHosts = route.Target.HostInfos
                                .Where(h => h.HostName != null && h.Host != null)
                                .ToList();

                    if (validHosts == null || validHosts.Count() <= 0)
                    {
                        _logger.LogError("Route[{0}] Not Valid: Valid Host Not found", route.RouteSettingId);
                        _logLib.ObjectErrorLogging(route);
                        continue;
                        //Todo question log no need fail
                    }

                    route.Target.HostInfos = validHosts;
                    NavRoutes.Add(route);
                }

                if (NavRoutes.Count() <= 0)
                {
                    _logger.LogError("ProxySetting Not Valid: Valid Route Not found");
                    //Todo return fail
                }

                proxy.RouteSettings = NavRoutes.ToList();
                _logger.LogDebug("Service: {0} filtered proxySetting : {1}", customHeader.Service, JsonConvert.SerializeObject(proxy));
            }
            catch (Exception ex)
            {
                _logger.LogError("SetNavRoutes fail ex:{0}", ex.ToString());
                throw;
            }
        }
        public void SetNavRoutesHostInfos()
        {
            try
            {
                var route = NavRoutes.Current;
                if (route != null)
                {
                    if (route.IsDefault)
                    {
                        route.Target.HostInfos = route.Target.HostInfos.Take(1).ToList();
                    }
                    else
                    {
                        if (route.Condition != null &&
                            route.Condition.CustomCode == customHeader.CustomCode &&
                            route.Condition.BusinessType == customHeader.BusinessType)
                        {
                            //no change
                        }
                        else if (NavRoutes.MoveNext)
                        {
                            SetNavRoutesHostInfos();
                        }
                        else
                        {
                            _logger.LogError("Target Not found: No Next NavRoutes, index: {0}", NavRoutes.CurrentIndex);
                            //todo return fail no Next Route
                        }
                    }
                }
                else
                {
                    _logger.LogError("Target Not found: No Valid current NavRoutes, index: {0}", NavRoutes.CurrentIndex);
                    //todo return fail
                }

                if (route.Target.HostInfos == null || route.Target.HostInfos.Count() == 0)
                {
                    _logger.LogError("Valid Target Not found: SetTargetServer Fail, index: {0}", NavRoutes.CurrentIndex);
                    //todo return fail
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
