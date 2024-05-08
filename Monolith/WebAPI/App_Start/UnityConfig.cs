using System.Configuration;
using System.Web.Http;
using Paramore.Brighter;
using Paramore.Brighter.MessagingGateway.Kafka;
using Paramore.Brighter.MsSql;
using Paramore.Brighter.Outbox.MsSql;
using Unity;
using Unity.AspNet.WebApi;
using WebAPI.DataAccess;
using WebAPI.Messaging;
using WebAPI.Messaging.Commands;
using WebAPI.Messaging.Events;
using WebAPI.Messaging.Handlers;
using WebAPI.Messaging.MessageMappers;
using WebAPI.Note;

namespace WebAPI
{
    public class UnityConfig
    {
        public static void RegisterComponents()
        {
            var diContainer = new UnityContainer().AddExtension(new Diagnostic());
            diContainer.RegisterType<INoteManager, Note.NoteManager>();

            

            // Brighter setup
            var subscriberRegistry = new SubscriberRegistry();
            subscriberRegistry.Register<SignNoteCommand, SignNoteCommandHandler>();

            var handlerFactory = new NoteHandlerFactory();

            var kafkaConnection = new KafkaMessagingGatewayConfiguration()
            {
                Name = "webapi",
                BootStrapServers = new[] { "localhost:19092" }
            };

            var kafkaPubs = new[]
            {
                new KafkaPublication()
                {
                    Topic = new RoutingKey("sign-note")
                }
            };
            IAmAProducerRegistry kafkaProducer = new KafkaProducerRegistryFactory(kafkaConnection, kafkaPubs).Create();

            var outgoingMessageMapperRegistry = new MessageMapperRegistry(new SimpleMessageMapperFactory(_ => new SignNoteCommandMessageMapper()));
            outgoingMessageMapperRegistry.Register<SignNoteCommand, SignNoteCommandMessageMapper>();
            
            //  new MsSqlSqlAuthConnectionProvider(new MsSqlConfiguration(ConfigurationManager.ConnectionStrings["NoteContext"].ConnectionString))
            string connectionString = ConfigurationManager.ConnectionStrings["NoteContext"].ConnectionString;

            var outbox = new MsSqlOutbox(new MsSqlConfiguration(connectionString, "Outbox"));

            var boxTransactionConnectionProvider = new EntityFwTransactionConnectionProvider(new NoteContext());
            diContainer.RegisterInstance(boxTransactionConnectionProvider);
            diContainer.RegisterType<IAmABoxTransactionConnectionProvider, EntityFwTransactionConnectionProvider>();

            IAmACommandProcessorBuilder commandProcBuilder = CommandProcessorBuilder.With()
                .Handlers(new HandlerConfiguration(subscriberRegistry, handlerFactory))
                .DefaultPolicy()
                .ExternalBus(new ExternalBusConfiguration(kafkaProducer, outgoingMessageMapperRegistry), outbox, boxTransactionConnectionProvider)
                .RequestContextFactory(new InMemoryRequestContextFactory());

            CommandProcessor commander = commandProcBuilder.Build();
            
            diContainer.RegisterInstance<IAmACommandProcessor>(commander);

            
            //var incomingMessageMapperRegistry = new MessageMapperRegistry(new ControlBusMessageMapperFactory());
            //incomingMessageMapperRegistry.Register<NoteSignedEvent, NoteSignedMessageMapper>();

            //var dispatcher = DispatchBuilder.With()
            //    .CommandProcessorFactory(() => new CommandProcessorProvider(commander))
            //    .MessageMappers(incomingMessageMapperRegistry, null)
            //    .DefaultChannelFactory(new ChannelFactory(new KafkaMessageConsumerFactory(kafkaConnection)));


            //diContainer.RegisterInstance<IDispatcher>(dispatcher.Build());



            var resolver = new UnityDependencyResolver(diContainer);
            
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
            
        }
    }
}