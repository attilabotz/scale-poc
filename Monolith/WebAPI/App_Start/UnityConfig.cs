using System.Web.Http;
using MassTransit;
using Unity;
using Unity.AspNet.WebApi;

namespace WebAPI
{
    public class UnityConfig
    {
        public static void RegisterComponents()
        {
            var diContainer = new UnityContainer();
            
            var busControl1 = Bus.Factory.CreateUsingInMemory(cfg =>
            {
                // TODO: Add Kafka configuration
            });

            diContainer.RegisterInstance<IBusControl>(busControl1);
            diContainer.RegisterInstance<IBus>(busControl1);
            
            busControl1.Start();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(diContainer);
        }
    }
}