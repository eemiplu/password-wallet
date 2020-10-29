using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

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

            ////convert byte array to string
            //return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
