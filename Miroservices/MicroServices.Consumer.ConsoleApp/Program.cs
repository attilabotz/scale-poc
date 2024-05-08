using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Paramore.Brighter;
using Paramore.Brighter.MessagingGateway.Kafka;
using Paramore.Brighter.ServiceActivator.Extensions.DependencyInjection;
using Paramore.Brighter.ServiceActivator.Extensions.Hosting;

namespace MicroServices.Consumer.ConsoleApp
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args)
                .UseConsoleLifetime()
                .Build();

            host.Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var subscription = new KafkaSubscription[]
                    {
                        new KafkaSubscription<SignNoteCommand>(
                            new SubscriptionName("dotnet8-consumer-console"),
                            channelName: new ChannelName("sign-note"),
                            routingKey: new RoutingKey("sign-note"),
                            groupId: "note-signer")
                    };

                    var consumerFactory = new KafkaMessageConsumerFactory(
                        new KafkaMessagingGatewayConfiguration()
                        {
                            Name = "note-signer-consumer",
                            BootStrapServers = new[] { "localhost:19092" }
                        }
                        );
                    
                    services.AddServiceActivator(o =>
                    {
                        o.Subscriptions = subscription;
                        o.ChannelFactory = new ChannelFactory(consumerFactory);
                    })
                    .AutoFromAssemblies();

                    services.AddHostedService<ServiceActivatorHostedService>();
                });
    }
}
