using System;
using System.Collections.Generic;

namespace OldTemplate.Model
{
    /// <summary>
    /// 代理設定
    /// </summary>
    public class ProxySetting
    {
        public string Version { get; set; }

        public List<RouteSetting>? RouteSettings { get; set; }

        //todo pending property
        public string Author { get; set; }
        public string ReviseDate { get; set; }
    }
}
