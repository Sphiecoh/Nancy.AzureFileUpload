using Nancy.TinyIoc;

namespace Nancy.AzureFileUpload
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
           container.Register<IStorage,AzureFileStorage>();
        }
    }
}