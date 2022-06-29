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
    var eventoEmail =  new EventingBasicConsumer(channel);
    var eventoLog =  new EventingBasicConsumer(channel);

    eventoEmail.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" [EMAIL] recebido '{0}'", message);
    };

    eventoLog.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" [LOG] recebido '{0}'", message);
    };

    channel.BasicConsume(queue: "errosGerais",
                            autoAck: true,
                            consumer: eventoLog);

    channel.BasicConsume(queue: "emailParaResponsavel",
                                autoAck: true,
                                consumer: eventoEmail);

    Console.ReadLine();
}