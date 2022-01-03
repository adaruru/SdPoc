using System.ComponentModel.DataAnnotations;

namespace OldTemplate.Lib.Common
{
    public class Enum
    {
        public enum RobinType
        {
            /// <summary>
            /// 加權輪巡負載平衡
            /// </summary>
            [Display(Name = "加權負載平衡")]
            WeightRobin = 0,

            /// <summary>
            /// 輪巡負載平衡
            /// </summary>
            [Obsolete("停用輪巡、隨機輪巡", true)]
            [Display(Name = "輪巡負載平衡")]
            RoundRobin = 10,

            /// <summary>
            /// 隨機負載平衡
            /// </summary>
            [Obsolete("停用輪巡、隨機輪巡", true)]
            [Display(Name = "隨機負載平衡")]
            RandomRobin = 11,
        }
    }
}
