using System;
using System.Collections.Generic;

namespace ProxyHost.Model
{
    public class ProxySetting
    {
        public string Version { get; set; }
        public List<RouteSetting> RouteSettings { get; set; }
    }

    public class RouteSetting
    {
        public string ClientName { get; set; }
        public string ServiceLine { get; set; }
        public string BusinesNo { get; set; }
        public string TargetUri { get; set; }
    }
}
