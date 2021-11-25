using System;
using System.Collections.Generic;

namespace ITSProxyService.Model
{
    public class ProxySetting
    {
        public string Version { get; set; }
        public List<RouteSetting> RouteSettings { get; set; }
    }

    public class RouteSetting
    {
        /// <summary>
        /// 路由Id
        /// </summary>
        public int RouteSettingId { get; set; }
        
        /// <summary>
        /// 路由設定優先
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 是否為預設 route
        /// </summary>
        public bool IsDefault { get; set; }

        public string ClientName { get; set; }
        public string ServiceLine { get; set; }
        public string BusinesNo { get; set; }
        public string TargetUri { get; set; }
    }
}
