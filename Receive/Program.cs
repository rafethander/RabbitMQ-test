using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Receive
{
    class Program
    {
        static void Main(string[] args)
        {
            var connFactory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = connFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "Rafet", durable: true, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (sender, ea) =>
                    {
                        var body = ea.Body.ToArray();//ByteArray geliyor queue den

                        var message = Encoding.UTF8.GetString(body);

                        Console.WriteLine($"Mesaj Alındı.İçerik : {message}");
                    };

                   
                    

                    //autoAck queue deki mesajın alındıgını işaretler queue ye baglı dıger consumer lar bunu gorur işlemez.
                    channel.BasicConsume(queue: "Rafet",
                                 autoAck: true,
                                 consumer: consumer);




                    //Connection ve channel ı admın panelınden gorebılmek ıcın.
                    //Thread.Sleep(TimeSpan.FromSeconds(20));
                }
            }

            Console.WriteLine("Enter a basınız");
            Console.ReadLine();
        }
    }
}
