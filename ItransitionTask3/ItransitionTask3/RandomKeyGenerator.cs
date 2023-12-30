using System.Security.Cryptography;

namespace task3;

public static class RandomKeyGenerator
{
    public static string Generate(int length)
    {
        byte[] key = new byte[length];
        using var numberGenerator = RandomNumberGenerator.Create();
        numberGenerator.GetBytes(key);
        return Convert.ToHexString(key);
    }
}