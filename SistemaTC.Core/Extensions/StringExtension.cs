using Newtonsoft.Json;

namespace SistemaTC.Core.Extensions;
public static class StringExtension
{
    public static T? AsNullableObject<T>(this string value)
    {
        if(string.IsNullOrEmpty(value)) return default;

        return JsonConvert.DeserializeObject<T>(value);
    }
}
