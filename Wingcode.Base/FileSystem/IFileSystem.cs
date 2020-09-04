using System.IO;
using System.Threading.Tasks;

namespace Wingcode.Base.FileSystem
{
    public interface IFileSystem
    {
        string Root { get; }

        string Extension { get; }

        void CreateFile(string name);

        bool FileExsist(string name);

        string ResolvePath(string name);

        bool DeleteFile(string name);

        FileStream GetFileStream(string name, bool write = false);

        Task<T> ReadFileAsync<T>(string fileName, bool sercure = false);

        Task WriteFileAsync<T>(T content, string fileName, bool append = false, bool sercure = false);
    }
}
