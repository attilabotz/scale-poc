using Paramore.Brighter;
using Paramore.Brighter.Extensions.DependencyInjection;
using Paramore.Brighter.Extensions.Hosting;
using Paramore.Brighter.MessagingGateway.Kafka;
using Paramore.Brighter.MsSql;
using Paramore.Brighter.Outbox.MsSql;
using WebAPI.Messaging;

namespace WebAPI.OutboxWorkerService.Net8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
         
            var kafkaConnection = new KafkaMessagingGatewayConfiguration()
            {
                Name = "webapi",
                BootStrapServers = new[] { "localhost:19092" },
                Debug = "broker, topic, msg"
            };
            var kafkaPubs = new[]
            {
                new KafkaPublication()
                {
                    Topic = new RoutingKey("sign-note"),
                    MakeChannels = OnMissingChannel.Assume
                }
            };
            IAmAProducerRegistry? kafkaProducer = new KafkaProducerRegistryFactory(kafkaConnection, kafkaPubs).Create();

            string connectionString = builder.Configuration.GetConnectionString("NoteContext");

            var outbox = new MsSqlOutbox(new MsSqlConfiguration(connectionString, "Outbox"));

            builder.Services
                .AddBrighter()
                .UseExternalBus(kafkaProducer)
                .AutoFromAssemblies(typeof(NoteHandlerFactory).Assembly)
                .UseExternalOutbox(outbox)
                .UseOutboxSweeper();

            builder.Services.AddHostedService<TimedOutboxSweeper>();

            var host = builder.Build();
            host.Run();
        }
    }
}