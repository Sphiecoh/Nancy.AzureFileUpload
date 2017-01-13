using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Nancy.AzureFileUpload
{
    public interface IStorage
    {
        Task UploadAsync(Stream fileStream, string filename);
        Task<IEnumerable<string>> GetFilesAsync();
    }
}