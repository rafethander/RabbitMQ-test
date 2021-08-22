using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace QueueProducer
{
    public class QueueProducer
    {

        public static void Publish(IModel channel)
        {
            channel.QueueDeclare("Rafet", true, false, false, null);

            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello ! Count: {count}" };

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish("", "Rafet", null, body);

                Thread.Sleep(TimeSpan.FromSeconds(1));
                count++;
            }
        }
    }
}
