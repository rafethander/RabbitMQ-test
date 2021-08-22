using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueueConsumer
{
    public static class DirectExchangesConsumers
    {

        public static void Consumer(IModel channel)
        {

            channel.ExchangeDeclare("Demo-Direct-Exchange", ExchangeType.Direct);
            channel.QueueDeclare("direct-queue", durable: true, exclusive: false, autoDelete: false,
                arguments:null);

            channel.QueueBind("direct-queue", "Demo-Direct-Exchange", "account.init");

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, ea) =>
            {
                var body = ea.Body.ToArray();//ByteArray geliyor queue den

                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Mesaj Alındı.İçerik : {message}");
            };

            channel.BasicConsume(queue: "direct-queue",
                                         autoAck: true,
                                         consumer: consumer);
        }
    }
}
