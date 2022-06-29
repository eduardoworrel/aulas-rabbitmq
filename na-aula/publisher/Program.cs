using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory() { 
    HostName = "143.244.137.227",
    UserName = "admin",
    Password = "devInRabbit"
    };

using(var connection = factory.CreateConnection()) //disposable
using(var channel = connection.CreateModel())
{
    channel.ExchangeDeclare("multiple-ew", ExchangeType.Direct);

    channel.QueueDeclare(queue: "textos",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

    channel.QueueBind(queue: "textos",
            exchange: "multiple-ew",
            routingKey: "textos"); 

    channel.QueueDeclare(queue: "numeros",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

    channel.QueueBind(queue: "numeros",
            exchange: "multiple-ew",
            routingKey: "numeros"); 

    channel.QueueDeclare(queue: "boleanos",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

    channel.QueueBind(queue: "boleanos",
            exchange: "multiple-ew",
            routingKey: "boleanos"); 
   

    string messageTexto = "Hello World!";
    
    string messageNumber = "123456789";
    
    string messageBoleana = "true";
    
    

    channel.BasicPublish(exchange: "multiple-ew",
                        routingKey: "textos",
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes(messageTexto));

    Console.WriteLine(" [x] Sent {0}", messageTexto);

    channel.BasicPublish(exchange: "multiple-ew",
                        routingKey: "numeros",
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes(messageNumber));

    Console.WriteLine(" [x] Sent {0}", messageNumber);

    channel.BasicPublish(exchange: "multiple-ew",
                        routingKey: "boleanos",
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes(messageBoleana));


    Console.WriteLine(" [x] Sent {0}", messageBoleana);


}
