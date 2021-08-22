using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

namespace Send
{
    class Program
    {
       
        static void Main(string[] args)
        {
            //guest: id 2. guest:şifre
            var connFactory = new ConnectionFactory() { Uri = new Uri("amqp://guest:guest@localhost:5672") }; //{ HostName = "localhost" };

            using (var connection = connFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    //durable : true çünkü message alınana kadar consumer tarafından  beklemesını ıstıyoruz
                    channel.QueueDeclare(queue: "Hello", durable: true, exclusive: false, autoDelete: false, arguments: null);

                    var message =new {Name="Producer",Message= "Welcome Hander" } ;

                    var body= Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                    channel.BasicPublish(exchange: "", routingKey: "Hello", basicProperties: null, body: body);


                    Console.WriteLine($"Mesaj gönderildi, İçerik:{message}");


                    //Connection ve channel ı admın panelınden gorebılmek ıcın.
                    //Thread.Sleep(TimeSpan.FromSeconds(20));
                }
            }

            Console.WriteLine("Enter a basınız");
            Console.ReadLine();
        }
    }
}
