using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace AlgoLibrary
{
    public class Hashing
    {

        private static readonly byte[] key = KeyDerivation.DeriveKey(Key.MyKey);

        public static string EncryptPassword(string password)
        {
            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = key;
                    aesAlg.Mode = CipherMode.ECB;

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor();

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(password);
                            }
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            } catch (Exception ex)
            {
                return "";
            }
        }

        public static string DecryptPassword(string encryptedPassword)
        {
            try
            {
                if (encryptedPassword == null) return "";
                byte[] cipherText = Convert.FromBase64String(encryptedPassword);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = key;
                    aesAlg.Mode = CipherMode.ECB;

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor();

                    using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            } catch (Exception ex)
            {
                return "";
            }
        }
    }
}
