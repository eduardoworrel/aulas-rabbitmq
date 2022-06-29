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
  channel.ExchangeDeclare("ew-logs",ExchangeType.Fanout);
  
  
  channel.QueueDeclare(queue:"errosGerais",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

  channel.QueueBind(queue: "errosGerais",
            exchange: "ew-logs",
            routingKey: "");


  channel.QueueDeclare(queue:"emailParaResponsavel",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);


  channel.QueueBind(queue: "emailParaResponsavel",
            exchange: "ew-logs",
            routingKey: "");

}