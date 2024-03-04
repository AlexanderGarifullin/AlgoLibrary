using System.Security.Cryptography;
using System.Text;

namespace AlgoLibrary
{
    public class Hashing
    {
        static int HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                string hashedPasswordString = builder.ToString();
                int hashedPasswordInt = Convert.ToInt32(hashedPasswordString.Substring(0, 8), 16);
                return hashedPasswordInt;
            }
        }
    }
}
