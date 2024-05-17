using Newtonsoft.Json;
using BCryptLib = BCrypt.Net.BCrypt;

namespace SistemaTC.Core.Extensions;
public static class StringExtension
{
    public static T? AsNullableObject<T>(this string value)
    {
        if(string.IsNullOrEmpty(value)) return default;

        return JsonConvert.DeserializeObject<T>(value);
    }
    public static string Hash(this string text)
    {
        return BCryptLib.HashPassword(text);
    }

    public static bool ValidHash(this string text, string hash)
    {
        return BCryptLib.Verify(text, hash);
    }
}
