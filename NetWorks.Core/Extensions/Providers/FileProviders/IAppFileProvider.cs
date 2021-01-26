using Microsoft.Extensions.FileProviders;
using System.Threading.Tasks;

namespace NetWorks.Core.Extensions.Providers.FileProviders
{
    public interface IAppFileProvider : IFileProvider
    {
        string GetFilePathToSave(string filePath);

        string GetFileNameWithoutExtension(string filePath);

        string GetFileName(string filePath);

        string GetFileExtension(string filePath);

        string GetDirectoryName(string path);

        bool FileExists(string filePath);

        void WriteAllBytes(string filePath, byte[] bytes);

        Task WriteAllBytesAsync(string filePath, byte[] bytes);
    }
}