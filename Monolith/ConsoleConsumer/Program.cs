using Confluent.Kafka;

namespace ConsoleConsumer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConsumerConfig
            {
                GroupId = "console-consumer",
                BootstrapServers = "localhost:19092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            
            using (var consumer = new ConsumerBuilder<string, string>(config).Build())
            {
                consumer.Subscribe("current-time");
                
                while (true)
                {
                    var consumeResult = consumer.Consume();
                    System.Console.WriteLine($"Consumed message '{consumeResult.Message.Value}' at: '{consumeResult.TopicPartitionOffset}'");
                }
            }
        }
    }
}