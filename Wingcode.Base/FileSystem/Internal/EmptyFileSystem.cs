using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wingcode.Base.FileSystem.Internal
{
    internal class EmptyFileSystem : IFileSystem
    {
        public string Root => throw new NotImplementedException();

        public string Extension => throw new NotImplementedException();

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

        public Task<T> ReadFileAsync<T>(string fileName, bool sercure = false)
        {
            throw new NotImplementedException();
        }

        public string ResolvePath(string name)
        {
            throw new NotImplementedException();
        }

        public Task WriteFileAsync<T>(T content, string fileName, bool append = false, bool sercure = false)
        {
            throw new NotImplementedException();
        }
    }
}
