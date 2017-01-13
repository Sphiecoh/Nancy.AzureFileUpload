using System.Linq;
using Serilog;

namespace Nancy.AzureFileUpload
{
    public class UploadModule : NancyModule
    {
        public UploadModule(IStorage storage)
        {
           Post("/file", async o =>
           {
               if (!Request.Files.Any())
                   return 400;
               foreach (var file in Request.Files)
               {
                   Log.Information($"uploading file {file.Name} ({file.Value.Length / 1000} KB)");
                   await storage.UploadAsync(file.Value, file.Name);
               }
               return Response.AsJson(new { message = "uploaded"});
           });

           Get("/files", async _ => Response.AsJson(new { Files =  await storage.GetFilesAsync()}));
        }
    }
}