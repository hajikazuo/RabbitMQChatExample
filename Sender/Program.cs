using Common;
using RabbitMQ.Client;
using System.Text.Json;

var factory = new ConnectionFactory { HostName = "localhost"};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

Console.WriteLine("Digite sua mensagem e aperte <ENTER>");

while (true)
{
    string message = Console.ReadLine();

    if (message == "")
        break;

    var user = new User() { Id = 1, Name = "Nilton" };
    message = JsonSerializer.Serialize(user);

    var body = System.Text.Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(exchange: "",
                         routingKey: "hello",
                         basicProperties: null,
                         body: body);
    Console.WriteLine($"[x] Enviado: {message}");
}
