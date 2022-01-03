using static OldTemplate.Lib.Common.Enum;

namespace OldTemplate.Model
{
    /// <summary>
    /// 路由目標陣列、埠號、負載平衡方式
    /// </summary>
    public class Target
    {
        public List<HostSetting> HostInfos { get; set; }

        public RobinType RobinType { get; set; }

        /// <summary>
        /// RoundRobin 輪巡負載平衡 起始位置
        /// </summary>
        public int Postion { get; set; } = 0;
        public int? Port { get; set; } = 0;
        public CustomHeader CustomHeader { get; set; }
    }
}
