using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Threading.Tasks;

namespace NetWorks.Core.Extensions.Providers.FileProviders
{
    public class AppFileProvider : PhysicalFileProvider, IAppFileProvider
    {
        public AppFileProvider(string root) : base(root)
        {
        }

        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public string GetDirectoryName(string path)
        {
            return Path.GetDirectoryName(path);
        }

        public string GetFileExtension(string filePath)
        {
            return Path.GetExtension(filePath);
        }

        public string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }

        public string GetFileNameWithoutExtension(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        public string GetFilePathToSave(string filePath)
        {
            var fileNoExtension = GetFileNameWithoutExtension(filePath);
            var extension = GetFileExtension(filePath);
            var directory = GetDirectoryName(filePath);

            int i = 1;

            while (FileExists(filePath))
            {
                var filename = string.Concat(fileNoExtension, "-", i, extension);
                filePath = Path.Combine(directory, filename);
                i++;
            }

            return filePath;
        }

        public void WriteAllBytes(string filePath, byte[] bytes)
        {
            File.WriteAllBytes(filePath, bytes);
        }

        public async Task WriteAllBytesAsync(string filePath, byte[] bytes)
        {
            await File.WriteAllBytesAsync(filePath, bytes);
        }
    }
}