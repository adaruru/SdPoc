using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json.Linq;

//參考h
//ttps://stackoverflow.com/questions/41653688/asp-net-core-appsettings-json-update-in-code

namespace PerformanceApi.Lib;

public static class SettingsHelpers
{
    public static void AddOrUpdateAppSetting<T>(T value, IWebHostEnvironment webHostEnvironment)
    {
        try
        {
            var settingFiles = new List<string> { "appsettings.json", $"appsettings.{webHostEnvironment.EnvironmentName}.json" };
            foreach (var item in settingFiles)
            {


                var filePath = Path.Combine(AppContext.BaseDirectory, item);
                string json = File.ReadAllText(filePath);
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                SetValueRecursively(jsonObj, value);

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, output);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error writing app settings | {ex.Message}", ex);
        }
    }


    private static void SetValueRecursively<T>(dynamic jsonObj, T value)
    {
        var properties = value.GetType().GetProperties();
        foreach (var property in properties)
        {
            var currentValue = property.GetValue(value);
            if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string) || property.PropertyType == typeof(decimal))
            {
                if (currentValue == null) continue;
                try
                {
                    jsonObj[property.Name].Value = currentValue;

                }
                catch (RuntimeBinderException)
                {
                    jsonObj[property.Name] = new JValue(currentValue);

                }
                continue;
            }
            try
            {
                if (jsonObj[property.Name] == null)
                {
                    jsonObj[property.Name] = new JObject();
                }

            }
            catch (RuntimeBinderException)
            {
                jsonObj[property.Name] = new JObject(new JProperty(property.Name));

            }
            SetValueRecursively(jsonObj[property.Name], currentValue);
        }
    }
}
