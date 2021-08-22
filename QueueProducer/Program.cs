using RabbitMQ.Client;
using System;

namespace QueueProducer
{
    class Program
    {
        static void Main(string[] args)
        {

            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    //QueueProducer.Publish(channel);
                    DirectExchangesProducers.Publish(channel);
                }
            }
        }
    }
}
