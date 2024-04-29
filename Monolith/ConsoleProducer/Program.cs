using System;
using System.Threading;
using Confluent.Kafka;

namespace ConsoleProducer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:19092"
            };
            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {
                while (true)
                {
                    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        break;
                    }

                    producer.Produce("current-time",
                        new Message<string, string> { Key = "CurrentTime", Value = DateTime.Now.ToString() });
                    Thread.Sleep(1000);
                }
            }
        }
    }
}