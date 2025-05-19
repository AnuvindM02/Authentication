namespace Authentication.Application.RabbitMq
{
    public interface IRabbitMqPublisher
    {
        void Publish<T>(string routingKey, T message);
    }
}
