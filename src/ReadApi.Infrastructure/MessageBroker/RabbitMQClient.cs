using RabbitMQ.Client;
using ReadApi.Core.Services;
using System.Text;

namespace ReadApi.Infrastructure.MessageBroker
{
    public class RabbitMQClient : IRabbitMQClient
    {
        private readonly IConnection _connection;
        private readonly RabbitMQ.Client.IModel _channel;

        public RabbitMQClient(IConnection connection)
        {
            _connection = connection;
            _channel = _connection.CreateModel();
            _channel.ConfirmSelect();

        }
        public void CloseConnection()
        {
            _connection?.Close();
        }

        public void Publish(string exchange, string routingKey, string payload)
        {
            var props = _channel.CreateBasicProperties();
            props.AppId = "ReadApi";
            props.Persistent = true;
            props.UserId = Environment.GetEnvironmentVariable("RABBIT_USER");
            props.MessageId = Guid.NewGuid().ToString("N");
            props.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
            _channel.QueueDeclare(queue: routingKey,
                           exclusive: false,autoDelete:false);
            var body = Encoding.UTF8.GetBytes(payload);
            _channel.BasicPublish(exchange, routingKey, props, body);
            _channel.WaitForConfirmsOrDie(new TimeSpan(0, 0, 5));
        }
    }
}
