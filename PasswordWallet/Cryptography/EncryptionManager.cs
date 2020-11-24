using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace PasswordWallet.Cryptography
{
    public class EncryptionManager
    {
        private static readonly String PEPPER = "ab0ced040039c37b25bcdde5cfb181ed550e99c8410661a2734398dcbaff64a623d0cdfbd4fcd90e59a066b949638d946cba7162c880065f39ac7de304d406dd";

        public string EncryptPassword(String password, bool passwordStoredAsHash, String salt)
        {
            String encryptedPassword;

            if (passwordStoredAsHash) //SHA512
            {
                encryptedPassword = CalculateSHA512(salt + PEPPER + password);
            }
            else //HMAC
            {
                encryptedPassword = CalculateHMAC(password, salt);
            }

            return encryptedPassword;
        }

        public string GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        public string CalculateSHA512(String text)
        {
            if (text.Equals(""))
                throw new ArgumentException();

            //calculate SHA512 hash from input
            SHA512 sha512 = SHA512.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(text);
            byte[] hash = sha512.ComputeHash(inputBytes);

            //convert byte array to hex strings
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public string CalculateHMAC(String text, String key)
        {
            if (text.Equals("") || key.Equals(""))
                throw new ArgumentException();

            byte[] inputBytes = Encoding.ASCII.GetBytes(text);
            byte[] keyInBytes = Encoding.ASCII.GetBytes(key);

            //initialize new instance of the HMACSHA256 class with specified key
            HMACSHA512 hmacsha512 = new HMACSHA512(keyInBytes);

            //calculate HMAC-SHA512 hash from input
            byte[] hash = hmacsha512.ComputeHash(inputBytes);

            //convert byte array to hex strings
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public string AESEncrypt(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public string AESDecrypt(string key, string cipherText)
        {
            if (cipherText.Length != 24)
                throw new ArgumentException();

            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
