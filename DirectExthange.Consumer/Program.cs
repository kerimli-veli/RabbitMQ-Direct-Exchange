using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Consumer");

ConnectionFactory factory = new ConnectionFactory();

factory.Uri = new("amqps://fpuoiyor:t8SZYf8YmxPfSnvxwPiWbKYJwjfmNWlW@goose.rmq2.cloudamqp.com/fpuoiyor");


using IConnection connection = await factory.CreateConnectionAsync();

using IChannel channel = await connection.CreateChannelAsync();



await channel.ExchangeDeclareAsync(exchange: "direct-example-wirt-my-group", type: ExchangeType.Direct);


string queueName = channel.QueueDeclareAsync().Result.QueueName;

await channel.QueueBindAsync(
    queue: queueName,
    exchange: "direct-example-wirt-my-group",
          routingKey: "direct-queue"
         );



AsyncEventingBasicConsumer consumer = new(channel);

await channel.BasicConsumeAsync(queue: queueName, autoAck: true, consumer: consumer);




consumer.ReceivedAsync += async (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};


Console.Read();