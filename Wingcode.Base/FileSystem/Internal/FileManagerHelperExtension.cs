using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Wingcode.Base.FileSystem.Internal
{
    internal static class FileManagerHelperExtension
    {

        private static byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8 };

        private static byte[] iv = { 1, 2, 3, 4, 5, 6, 7, 8 };

        public static CryptoStream GetCryptoStream(this IFileManager fileManager, FileStream fileStream, bool write = false) 
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            if (write)
                return new CryptoStream(fileStream, provider.CreateEncryptor(key, iv), CryptoStreamMode.Write);
            else
                return new CryptoStream(fileStream, provider.CreateEncryptor(key, iv), CryptoStreamMode.Write);
        }
    }
}
