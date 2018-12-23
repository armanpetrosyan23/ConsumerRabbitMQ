using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsumerRabbitMQ
{
    class Program
    {


        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",

            };
            using (var connection = factory.CreateConnection())

            using (var channel = connection.CreateModel())
            {
                                                 

                var consumer = new EventingBasicConsumer(channel);

                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                   // Thread.Sleep(500);
                    Console.WriteLine(" [x] Received {0}", message);
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                };

                channel.BasicConsume(queue: "Hello",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

                Console.ReadLine();
            }
        }
    }
}
