using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { 
    HostName = "143.244.137.227",
    UserName = "admin",
    Password = "devInRabbit"
    };

using(var connection = factory.CreateConnection()) //disposable
using(var channel = connection.CreateModel())
{
    var consumerText = new EventingBasicConsumer(channel);
    var consumerNum = new EventingBasicConsumer(channel);
    var consumerBool = new EventingBasicConsumer(channel);
    
    consumerText.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" [Texto] recebido {0}", message);
    };
   
    channel.BasicConsume(queue: "textos",
                            autoAck: true,
                            consumer: consumerText);


    consumerNum.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" [Numeros] recebido {0}", message);
    };
   
    channel.BasicConsume(queue: "numeros",
                            autoAck: true,
                            consumer: consumerNum);

    consumerBool.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" [Booleano] recebido {0}", message);
    };
   
    channel.BasicConsume(queue: "boleanos",
                            autoAck: true,
                            consumer: consumerBool);

    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
}