using System.Web.Http;
using Notes;
using Unity;
using Unity.AspNet.WebApi;

namespace WebAPI
{
    public class UnityConfig
    {
        public static void RegisterComponents()
        {
            var diContainer = new UnityContainer().AddExtension(new Diagnostic());
            diContainer.RegisterType<INoteManager, NoteManager>();
            diContainer.RegisterType<INotePublisher, NotePublisher>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(diContainer);
        }
    }
}