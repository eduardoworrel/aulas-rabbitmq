using RabbitMQ.Client;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {

        var factory = new ConnectionFactory()
        {
            HostName = "143.244.137.227",
            UserName = "admin",
            Password = "devInRabbit"
        };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            foreach(var item in args){

               channel.BasicPublish(exchange: "ew-frases",
                        routingKey: "ew-frases",
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes(item));
            }

        }
    }
}