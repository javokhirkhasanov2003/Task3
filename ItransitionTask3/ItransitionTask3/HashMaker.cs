using System.Security.Cryptography;
using System.Text;

namespace task3;

public class HashMaker
{
    public string GetHash(string move, string secretKey) 
    {
        var messageBytes = Encoding.UTF8.GetBytes(move);
        var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
        
        using var hmac = new HMACSHA256(secretKeyBytes);
        var hashBytes = hmac.ComputeHash(messageBytes);
        var hash = Convert.ToHexString(hashBytes);
        
        return hash;
    }
}