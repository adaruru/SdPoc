namespace OldTemplate.Model
{
    /// <summary>
    /// 路由設定
    /// </summary>
    public class RouteSetting
    {
        public string Service { get; set; }
        public int? Port { get; set; }
        public int RouteSettingId { get; set; }

        /// <summary>
        /// 路由設定優先
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 是否為預設 true 則不參考 Condition 並導向第一筆 Host
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 路由目標陣列、埠號、負載平衡方式
        /// </summary>
        public Target? Target { get; set; }

        /// <summary>
        /// 路由情境判斷
        /// </summary>
        public Condition Condition { get; set; }


    }
}
