using Confluent.Kafka;

var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "mail-consumer-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

consumer.Subscribe("test");

CancellationTokenSource cancellationToken = new();

try
{
    while (true)
    {
        var consumeResult = consumer.Consume(cancellationToken.Token);
        if (consumeResult.Message != null)
        {
            var correo = consumeResult.Message.Value;
            Console.WriteLine($"Mensaje recibido: {correo}");
        }

    }

}
catch (OperationCanceledException)
{
    consumer.Close();
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
