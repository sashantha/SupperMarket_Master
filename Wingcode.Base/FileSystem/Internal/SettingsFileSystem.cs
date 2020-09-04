using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wingcode.Base.FileSystem.Internal
{
    internal class SettingsFileSystem : IFileSystem
    {
        public string Root { get; private set; }

        public string Extension { get; private set; }

        public SettingsFileSystem(string root, string extension)
        {
            Root = root;
            Extension = extension;
            if (!Directory.Exists(Root))
                Directory.CreateDirectory(Root);
        }

        public void CreateFile(string name)
        {
            throw new NotImplementedException();
        }

        public bool DeleteFile(string name)
        {
            throw new NotImplementedException();
        }

        public bool FileExsist(string name)
        {
            throw new NotImplementedException();
        }

        public FileStream GetFileStream(string name, bool write = false)
        {
            throw new NotImplementedException();
        }

        public string ResolvePath(string name)
        {
            throw new NotImplementedException();
        }

        public Task<T> ReadFileAsync<T>(string fileName, bool sercure = false)
        {
            throw new NotImplementedException();
        }

        public Task WriteFileAsync<T>(T content, string fileName, bool append = false, bool sercure = false)
        {
            throw new NotImplementedException();
        }
    }
}
