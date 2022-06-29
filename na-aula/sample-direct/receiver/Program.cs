using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { 
    HostName = "143.244.137.227",
    UserName = "admin",
    Password = "devInRabbit"
    };

using(var connection = factory.CreateConnection()) 
using(var channel = connection.CreateModel())
{
    var evento =  new EventingBasicConsumer(channel);

    evento.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" [OK] recebido '{0}'", message);
    };

    channel.BasicConsume(queue: "ew-frases",
                            autoAck: true,
                            consumer: evento);

    Console.ReadLine();
}