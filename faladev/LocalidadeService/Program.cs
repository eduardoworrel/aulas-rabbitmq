using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using System;
using System.Text;
using Models;

var factory = new ConnectionFactory() { 
                HostName = "143.244.137.227",
                UserName = "admin",
                Password = "devInRabbit"
                };
                    
using(var connection = factory.CreateConnection())
using(var channel = connection.CreateModel())
{
   
    var consumer = new EventingBasicConsumer(channel);
    
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var publicacao = JsonConvert.DeserializeObject
        <PublicacaoRefinada>(Encoding.UTF8.GetString(body));
        
        publicacao.CidadeDaPublicacao = "Macapá";
        string uri = $"http://localhost:5228/api/PublicacaoRefinada";
        var cliente = new HttpClient();

        var pairs = new List<KeyValuePair<string, string>> { };

        foreach (var prop in publicacao.GetType().GetProperties())
        {
            pairs.Add(new KeyValuePair<string, string>(prop.Name, prop.GetValue(publicacao, null)?.ToString() ?? ""));
        }

        var content = new FormUrlEncodedContent(pairs);

        var httpResponse = cliente.PostAsync(uri, content).Result;

        if (httpResponse.IsSuccessStatusCode)
        {
             
        }else{

            Console.WriteLine(" [x] erro ao enviar dados a API:{0}",httpResponse.StatusCode);
        }

    };
    //SE NÃO DEFINO O BIND, O EXCHANGE VIRA O PADRÃO E O ROUTING KEY VIRA O NAME DA QUEUE
    channel.BasicConsume(queue: "refinaTextoBruto",
                            autoAck: true,
                            consumer: consumer);

    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
}

