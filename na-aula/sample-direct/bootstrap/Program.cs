using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory() { 
    HostName = "143.244.137.227",
    UserName = "admin",
    Password = "devInRabbit"
    };

using(var connection = factory.CreateConnection()) 
using(var channel = connection.CreateModel())
{
  channel.ExchangeDeclare("ew-frases",ExchangeType.Direct);
  
  channel.QueueDeclare(queue:"ew-frases",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

  channel.QueueBind(queue: "ew-frases",
            exchange: "ew-frases",
            routingKey: "ew-frases");
}