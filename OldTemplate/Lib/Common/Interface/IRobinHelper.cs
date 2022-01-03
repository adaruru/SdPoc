using AspNetCore.Proxy;
using OldTemplate.Model;

namespace OldTemplate.Lib.Common
{
    public interface IRobinHelper
    {
        string GetEndpoint(Target target);
    }
}