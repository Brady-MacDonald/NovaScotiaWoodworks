using System;
using System.Security.Cryptography;
using System.Text;

namespace NovaScotiaWoodworks.AccountManager
{
    public class PasswordHash
    {
        /// <summary>
        /// Generate a unique salt for each password
        /// </summary>
        /// <param name="size">Determines the size of the salt</param>
        /// <returns></returns>
        internal static string CreateSalt(int size)
        {
            var saltBytes = new byte[size];

            //If an object implements IDisposable it should have Dispose() called on it
            //Or have is enclosed in a using block
            using (var randomProvider = new RNGCryptoServiceProvider())
            {
                randomProvider.GetNonZeroBytes(saltBytes);
            }

            return Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        /// Hashs with salt user password to be stored in database
        /// </summary>
        /// <param name="password">User entered password</param>
        /// <param name="salt">salt for user specific password</param>
        /// <returns>Hashed user password</returns>
        internal static string GetStringSha256Hash(string password, string salt)
        {
            if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(salt))
                return String.Empty;

            password = password + salt;
            using (var sha = new SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }
    }
}
