using System.Web.Http;
using MassTransit;
using MassTransit.KafkaIntegration.Configuration;
using MassTransit.Util;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Note.Events;

namespace WebAPI
{
    public class MassTransitConfig
    {
        public static void Register()
        {
            
            var services = new ServiceCollection();
            
            services.AddMassTransit(x =>
            {
                const string topicName = "new-note";
                const string consumerGroup = "your-consumer-group";
                const string kafkaBrokerServers = "localhost:19092"; 
            
                x.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context)); 
            
                x.AddRider(rider =>
                {
                    //rider.AddConsumer<YourKafkaMessageConsumer>(); // Replace with your consumer class
                    rider.UsingKafka((context, k) =>
                    {
                        k.Host(kafkaBrokerServers);
            
                        k.TopicEndpoint<NewNoteReceived>(topicName, consumerGroup, e =>
                        {
                            //e.ConfigureConsumer<YourKafkaMessageConsumer>(context); // Replace with your consumer class
                            e.CreateIfMissing();
                        });
                    });
                });
            });
            
            var provider = services.BuildServiceProvider();
            
            var busControl = provider.GetRequiredService<IBusControl>();
            
            TaskUtil.Await(() => busControl.StartAsync());
        }
    }
}