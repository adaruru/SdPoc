using OldTemplate.Model;

namespace OldTemplate.Lib.Common
{
    public interface IConfigHelper
    {
        string Container { get; set; }

        T GetConfig<T>(FileArg fileArg);
        byte[] MockFileProvider(FileArg fileArg);
        void SetDataToCache(string key, byte[] data);
    }
}