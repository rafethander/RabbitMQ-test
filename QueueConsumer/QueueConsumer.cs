using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueueConsumer
{
    public class QueueConsumer
    {

        public static void Consumer(IModel channel)
        {
            channel.QueueDeclare(queue: "Rafet", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, ea) =>
                        {
                            var body = ea.Body.ToArray();//ByteArray geliyor queue den

                        var message = Encoding.UTF8.GetString(body);

                            Console.WriteLine($"Mesaj Alındı.İçerik : {message}");
                        };

            channel.BasicConsume(queue: "Rafet",
                                         autoAck: true,
                                         consumer: consumer);


        }
    }
}
