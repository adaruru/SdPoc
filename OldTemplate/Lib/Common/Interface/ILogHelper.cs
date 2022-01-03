
using Flurl;

namespace OldTemplate.Lib.Common
{
    public interface ILogHelper
    {
        public void ObjectDebugLogging<T>(T data, string dataName = null);
        public void ObjectErrorLogging<T>(T data, string dataName = null);
    }
}