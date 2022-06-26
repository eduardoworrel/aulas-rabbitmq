using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

var factory = new ConnectionFactory() { 
    HostName = "localhost",
    UserName = "admin",
    Password = "admin"
    };
    
using(var connection = factory.CreateConnection())
using(var channel = connection.CreateModel())
{
    
    channel.ExchangeDeclare("logs", ExchangeType.Fanout);

    var consumerA = new EventingBasicConsumer(channel);
                            
    consumerA.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" [A] Received {0}", message);
    };

    channel.BasicConsume(queue: "helloA",
                            autoAck: true,
                            consumer: consumerA);


    var consumerB = new EventingBasicConsumer(channel);
    
    consumerB.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" [B] Received {0}", message);
    };

    channel.BasicConsume(queue: "helloB",
                            autoAck: true,
                            consumer: consumerB);


 
    var consumerC = new EventingBasicConsumer(channel);
        
    consumerC.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" [C] Received {0}", message);
    };

    channel.BasicConsume(queue: "helloC",
                            autoAck: true,
                            consumer: consumerC);


 
    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
}