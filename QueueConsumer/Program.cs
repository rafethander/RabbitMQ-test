using RabbitMQ.Client;
using System;

namespace QueueConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var connFactory = new ConnectionFactory() { HostName = "localhost" };

            // Sen Projesındekinden farklı using leri kapattık olusturdugumuz baglantılar Dispose olmasın diye.
            var connection = connFactory.CreateConnection();
            var channel = connection.CreateModel();
            //QueueConsumer.Consumer(channel);
            DirectExchangesConsumers.Consumer(channel);


            Console.WriteLine("Started");
            Console.ReadLine();
        }
    }
}
