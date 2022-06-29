using Infra;

using (var db = new MyDbContext())
{
    Console.Write("Tipo de mensagem: ");
    var key = Console.ReadLine();
    Console.Write("Conteudo da mensagem: ");
    var value = Console.ReadLine();

    var customMessage = new CustomMessage { Key= key, Value = value };
    db.CustomMessages.Add(customMessage);
    db.SaveChanges();


    var query = from b in db.CustomMessages
                orderby b.Value
                select b;

    Console.WriteLine("All Messages in the database:");
    foreach (var item in query)
    {
        Console.WriteLine(item.Value);
    }

    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}