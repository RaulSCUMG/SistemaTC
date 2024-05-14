using BCryptLib = BCrypt.Net.BCrypt;

namespace SistemaTC.Core;
public static class Encryptor
{
    public static string Hash(this string text)
    {
        return BCryptLib.HashPassword(text);
    }

    public static bool ValidHash(this string text, string hash)
    {
        return BCryptLib.Verify(text, hash);
    }
}
