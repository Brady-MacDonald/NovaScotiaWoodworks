using NovaScotiaWoodworks.Models;
using System;
using System.Security.Cryptography;
using System.Text;

namespace NovaScotiaWoodworks.AccountManager
{
    public class PasswordHash
    {


        private string PasswordSalt
        {
            get
            {
                var rng = new RNGCryptoServiceProvider();
                var buff = new byte[32];
                rng.GetBytes(buff);
                return Convert.ToBase64String(buff);
            }
        }

        private string EncodePassword(string password, string salt)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] src = Encoding.Unicode.GetBytes(salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inarray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inarray);
        }

        private const int SaltSize = 16;
        private const int HashSize = 20;

        public static string HashPassword(string password)
        {

            //Generate salt value
            byte[] salt = new byte[SaltSize];
            byte[] textData = System.Text.Encoding.UTF8.GetBytes(password);

            //var rng = new RSACryptoServiceProvider().GetBytes(textData, salt);

            //Get the hash value
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            //Combine the salt and password bytes
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            //Tun combined salt and hash into string for storage
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }

        internal static string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }
    }
}
