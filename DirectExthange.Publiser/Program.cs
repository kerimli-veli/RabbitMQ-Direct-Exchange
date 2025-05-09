using RabbitMQ.Client;
using System.Text;

Console.WriteLine("Publiser");

ConnectionFactory factory = new();

factory.Uri = new("amqps://fpuoiyor:t8SZYf8YmxPfSnvxwPiWbKYJwjfmNWlW@goose.rmq2.cloudamqp.com/fpuoiyor");

using IConnection connection = await factory.CreateConnectionAsync();



using IChannel channel = await connection.CreateChannelAsync();


await channel.ExchangeDeclareAsync(exchange: "direct-example-wirt-my-group", type: ExchangeType.Direct);


while (true)
{
    Console.WriteLine("Mesaj: ");
    string message = Console.ReadLine();
    byte[]  byteMessage = Encoding.UTF8.GetBytes(message);

    await channel.BasicPublishAsync(
          exchange: "direct-example-wirt-my-group",
          routingKey: "direct-queue",
          body:byteMessage
         );
}