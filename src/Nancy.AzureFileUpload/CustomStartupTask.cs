using Serilog;
using Nancy.Bootstrapper;

namespace Nancy.AzureFileUpload
{
    public class CustomStartupTask : IApplicationStartup
    {
        public void Initialize(IPipelines pipelines)
        {
            pipelines.OnError.AddItemToEndOfPipeline((context,exception) =>{
                Log.Error(exception,"An error occurer");
                    return null;
            });
        }
    }
}