using System;
using System.Security.Cryptography;
using System.Text;

namespace NovaScotiaWoodworks.AccountManager
{
    public class PasswordHash
    {
        /// <summary>
        /// Hashs user password to be stored in database
        /// </summary>
        /// <param name="text">User entered password</param>
        /// <returns>Hashed user password</returns>
        internal static string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }
    }
}
