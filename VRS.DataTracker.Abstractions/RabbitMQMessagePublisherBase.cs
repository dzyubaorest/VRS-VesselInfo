using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using VRS.DataTracker.Abstractions.Dto.Settings;

namespace VRS.DataTracker.Abstractions
{
    public abstract class RabbitMQMessagePublisherBase<TMessage>
    {
        private readonly RabbitMQSettings settings;
        private readonly string queueName;

        protected RabbitMQMessagePublisherBase(RabbitMQSettings settings, string queueName)
        {
            this.settings = settings;
            this.queueName = queueName;
        }

        protected void Publish(TMessage message)
        {
            var factory = new ConnectionFactory() { HostName = settings.Host };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
