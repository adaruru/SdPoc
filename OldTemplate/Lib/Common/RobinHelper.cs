using AspNetCore.Proxy;
using OldTemplate.Lib.Biz;
using OldTemplate.Model;
using static OldTemplate.Lib.Utils.TargetUtil;
using static OldTemplate.Lib.Common.Enum;

namespace OldTemplate.Lib.Common
{
    public class RobinHelper : IRobinHelper
    {
        private readonly ILogger<RobinHelper> _logger;
        public RobinHelper(ILogger<RobinHelper> logger)
        {
            _logger = logger;
        }

        public string GetEndpoint(Target target)
        {
            var host = string.Empty;

            if (target == null || target.HostInfos == null)
            {
                return "";
            }
            else if (target.HostInfos.Count() == 1)
            {
                host = target.HostInfos[0].Host;
            }
            else if (target.RobinType == RobinType.WeightRobin)
            {
                host = target.HostInfos[0].Host;
            }
            else
            {
                host = target.HostInfos[0].Host;
            }

            var endpoint = new UriBuilder()
            {
                Scheme = "https",
                Host = host,
                Port = target.Port ?? 80
            };
            return endpoint.ToString();
        }

        [Obsolete("停用輪巡、隨機輪巡", true)]
        public bool TryGetSavedTarget(CustomHeader customHeader, out Target target)
        {
            var result = SavedTargets.Find(delegate (Target ta)
            {
                return ta.CustomHeader.Service == customHeader.Service &&
                ta.CustomHeader.CustomCode == customHeader.CustomCode &&
                ta.CustomHeader.BusinessType == customHeader.BusinessType;
            });

            if (result != null)
            {
                target = result;
                return true;
            }
            else
            {
                target = new Target();
                return false;
            }
        }

        [Obsolete("停用輪巡、隨機輪巡", true)]
        public string GetRoundRobin(Target target, bool isSaved)
        {
            if (target.Postion == target.HostInfos.Count())
            {
                target.Postion = 0;
            }
            return target.HostInfos[target.Postion++].Host;
        }

        [Obsolete("停用輪巡、隨機輪巡", true)]
        public string GetRandomRobin(Target target, bool isSaved)
        {
            var rand = new Random();
            return target.HostInfos[rand.Next(target.HostInfos.Count())].Host;
        }
    }
}
