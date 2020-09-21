using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Wingcode.Base.Security
{
    /// <summary>
    /// Helpers for the <see cref="SecureString"/> class
    /// </summary>
    public static class SecureStringHelpers
    {
        /// <summary>
        /// Unsecures a <see cref="SecureString"/> to plain text
        /// </summary>
        /// <param name="secureString">The secure string</param>
        /// <returns></returns>
        public static string Unsecure(this SecureString secureString)
        {
            // Make sure we have a secure string
            if (secureString == null)
                return string.Empty;

            // Get a pointer for an unsecure string in memory
            var unmanagedString = IntPtr.Zero;

            try
            {
                // Unsecures the password
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                // Clean up any memory allocation
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public static string EncordPassword(this string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string md5Hash2 = GetMd5Hash(md5Hash, password);
                if (VerifyMd5Hash(md5Hash, password, md5Hash2))
                {
                    return md5Hash2;
                }
                return string.Empty;
            }
        }

        public static bool ComparePassword(this string password, string savedPassword)
        {
            using (MD5 md5Hash = MD5.Create())
            {               
                if (VerifyMd5Hash(md5Hash, password, savedPassword))
                {
                    return true;
                }
                return false;
            }
        }
        private static string GetMd5Hash(MD5 md5Hash, string inputString)
        {
            byte[] array = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(inputString));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                stringBuilder.Append(array[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        private static bool VerifyMd5Hash(MD5 md5Hash, string inputString, string hash)
        {
            string md5Hash2 = GetMd5Hash(md5Hash, inputString);
            StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
            if (ordinalIgnoreCase.Compare(md5Hash2, hash) == 0)
            {
                return true;
            }
            return false;
        }
    }
}
