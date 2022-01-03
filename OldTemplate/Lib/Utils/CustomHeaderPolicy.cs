using OldTemplate.Model;
namespace OldTemplate.Lib.Utils
{
    public static class CustomHeaderPolicy
    {
        public static IDictionary<string, string> SetHeaders { get; }
            = new Dictionary<string, string>() { };

        public static IDictionary<string, string> AddHeaders { get; }
            = new Dictionary<string, string>() {
                { "ServiceUniqKey",Guid.NewGuid().ToString()}
            };

        //filter for dangerous headers
        public static ISet<string> RemoveHeaders { get; }
            = new HashSet<string>(){
                "RemoveKey1",
                "RemoveKey2"
            };
    }
}