using MicroService.Consumer.DataAccess;
using MicroServices.NoteSigner.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Paramore.Brighter;
using Paramore.Brighter.Extensions.DependencyInjection;
using Paramore.Brighter.Inbox.MsSql;
using Paramore.Brighter.MessagingGateway.Kafka;
using Paramore.Brighter.MsSql;
using Paramore.Brighter.ServiceActivator.Extensions.DependencyInjection;
using Paramore.Brighter.ServiceActivator.Extensions.Hosting;

namespace MicroServices.NoteSigner.ConsoleApp
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
                    services.AddDbContext<NoteSignerContext>(options
                    =>
                    {
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("NoteSignerDb"));
                    }, ServiceLifetime.Singleton);

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
                            BootStrapServers = new[] { "localhost:19092" },
                            Debug = "cgrp, topic, fetch"
                        }
                        );

                    var sqlConfiguration =
                        new MsSqlConfiguration(hostContext.Configuration.GetConnectionString("NoteSignerDb"),
                        outBoxTableName: "Outbox",
                        inboxTableName: "Inbox");
                    var sqlInbox = new MsSqlInbox(sqlConfiguration);

                    services.AddServiceActivator(o =>
                    {
                        o.Subscriptions = subscription;
                        o.ChannelFactory = new ChannelFactory(consumerFactory);
                    })
                    .AutoFromAssemblies()
                    .UseExternalInbox(sqlInbox, new InboxConfiguration(
                        scope: InboxScope.All,
                        onceOnly: true,
                        actionOnExists: Paramore.Brighter.Inbox.OnceOnlyAction.Throw
                        ));

                    services.AddHostedService<ServiceActivatorHostedService>();


                })
                .ConfigureLogging((hc, logging) =>
                {
                    logging.SetMinimumLevel(LogLevel.Debug);
                    logging.AddConsole();
                });
    }
}
