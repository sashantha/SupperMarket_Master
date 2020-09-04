using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wingcode.Base.FileSystem.Internal;

namespace Wingcode.Base.FileSystem
{
    public class FileManager : IFileManager
    {

        private readonly string name = "WCMS";

        private readonly Dictionary<FileSystemType, IFileSystem> Register = new Dictionary<FileSystemType, IFileSystem>();

        public string ExecutionRoot { get; private set; }

        public string AppDataRoot { get; private set; }

        public string AppDataLocalRoot { get; private set; }

        public string DocumentRoot { get; private set; }

        public string LogicalDriveRoot { get; private set; }

        public FileManager()
        {
            ExecutionRoot = Directory.GetCurrentDirectory();
            AppDataRoot = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            AppDataLocalRoot = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            DocumentRoot = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            LogicalDriveRoot = GetLogicalDrivePath();
            RegisterFileSystem(FileSystemType.physical, new PhysicalFileSystem() { Root = Path.Combine(LogicalDriveRoot, name), Extension = ".txt" });
            RegisterFileSystem(FileSystemType.settings, new SettingsFileSystem(Path.Combine(AppDataLocalRoot,name,"Settings"),".wstg"));
            RegisterFileSystem(FileSystemType.log, new LogFileSystem(Path.Combine(AppDataRoot,name,"Logs"),".wlog"));
        }

        private string GetLogicalDrivePath()
        {
            DriveInfo[] driveInfos = DriveInfo.GetDrives();
            string sysD = Environment.GetFolderPath(Environment.SpecialFolder.Windows).Substring(0, 1);
            DriveInfo drive = driveInfos.OrderByDescending(d => d.AvailableFreeSpace).FirstOrDefault(d => !d.Name.Equals(sysD));
            string selectedPath = Path.Combine(drive.Name, name);
            if (!Directory.Exists(selectedPath))
                Directory.CreateDirectory(selectedPath);
            return selectedPath;
        }

        public IFileSystem GetFileSystem(FileSystemType key)
        {
            if (!Register.ContainsKey(key))
                return new EmptyFileSystem();
            return Register[key];
        }

        public IFileSystem GetPhysicalFileSystem(string rootPath, string extension)
        {
            PhysicalFileSystem physicalFileSystem = Register[FileSystemType.physical] as PhysicalFileSystem;
            physicalFileSystem.Root = rootPath;
            physicalFileSystem.Extension = extension;
            return physicalFileSystem;
        }

        public async Task<T> ReadFileAsync<T>(FileSystemType fileSystemType, string fileName, bool sercure = false)
        {
            IFileSystem fileSystem = Register[fileSystemType];
            return await fileSystem.ReadFileAsync<T>(fileName, sercure);
        }

        public void RegisterFileSystem(FileSystemType key, IFileSystem fileSystem)
        {
            if (!Register.ContainsKey(key))
                Register.Add(key, fileSystem);
        }

        public async Task WriteFileAsync<T>(T content, FileSystemType fileSystemType, string fileName, bool append = false, bool sercure = false)
        {
            IFileSystem fileSystem = Register[fileSystemType];
            await fileSystem.WriteFileAsync<T>(content, fileName, append, sercure);
        }
    }
}
