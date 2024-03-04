using System.Security.Cryptography;
using System.Text;

namespace AlgoLibrary
{
    public class KeyDerivation
    {
        public static byte[] DeriveKey(string secret)
        {
            using (var sha256 = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(Key.MyKeyDerivitation));
            }
        }
    }
}
