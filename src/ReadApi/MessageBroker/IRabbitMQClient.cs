namespace ReadApi.MessageBroker
{
    public interface IRabbitMQClient
    {
        void Publish(string exchange, string routingKey, string payload);
        void CloseConnection();
    }
}