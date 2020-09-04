using System.IO;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Wingcode.Base.FileSystem
{
    /// <summary>
    /// Handles reading/writing and querying the file system
    /// </summary>
    public interface IFileManager
    {

        string ExecutionRoot { get; }
        string AppDataRoot { get; }
        string AppDataLocalRoot { get; }
        string DocumentRoot { get; }
        string LogicalDriveRoot { get; }

        void RegisterFileSystem(FileSystemType key, IFileSystem fileSystem);

        IFileSystem GetFileSystem(FileSystemType key);

        IFileSystem GetPhysicalFileSystem(string rootPath, string extension);

        Task<T> ReadFileAsync<T>(FileSystemType fileSystemType, string fileName, bool sercure = false);

        Task WriteFileAsync<T>(T content, FileSystemType fileSystemType, string fileName, bool append = false, bool sercure = false);

    }
}
