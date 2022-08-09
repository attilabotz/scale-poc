using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MassTransit;
using MassTransit.KafkaIntegration;
using MassTransit.KafkaIntegration.Configuration;
using MassTransit.Transports;

namespace MonolithFw47
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IBusControl _bus;
        private static BusHandle _busHandle;
        private static IKafkaRider _rider;
        private static RiderHandle _riderHandle;

        public static  IBus Bus => _bus;
        public static IKafkaRider Rider => _rider;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _bus = MassTransit.Bus.Factory.CreateUsingRabbitMq(x =>
            {
                x.Host(new Uri(ConfigurationManager.AppSettings["RabbitMQHost"]), h =>
                {
                    h.Username("guest");
                    h.Password("asdqwe123");
                });
            });
      
            
            _busHandle = MassTransit.Util.TaskUtil.Await<BusHandle>(() => _bus.StartAsync());
            
        }
    }
}
