using System;

namespace ProxyHost.Model
{
    public class RouteConfig
    {
        public string ServiceLine { get; set; }
        public string BusinessNo { get; set; }
        public string ClientName { get; set; }
        public Uri TargetUri { get; set; }

    }
}
