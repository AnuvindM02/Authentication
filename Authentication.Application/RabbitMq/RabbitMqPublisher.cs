using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Authentication.Application.RabbitMq
{
    public class RabbitMqPublisher : IRabbitMqPublisher, IDisposable
    {
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMqPublisher(IConfiguration configuration)
        {
            _configuration = configuration;
            string hostName = _configuration["RabbitMq:HostName"]!;
            string userName = _configuration["RabbitMq:UserName"]!;
            string password = _configuration["RabbitMq:Password"]!;
            string port = _configuration["RabbitMq:Port"]!;

            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = hostName,
                UserName = userName,
                Password = password,
                Port = Convert.ToInt32(port)
            };
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }

        public void Publish<T>(string routingKey, T message)
        {
            string messageJson = JsonSerializer.Serialize(message);
            byte[] messagebodyInBytes = Encoding.UTF8.GetBytes(messageJson);

            string exchangeName = _configuration["RabbitMQ:Auth_Exchange"]!;
            _channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, durable: true);

            _channel.BasicPublish(exchange: exchangeName,
                routingKey: routingKey,
                body: messagebodyInBytes);
        }
    }
}
