using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using VRS.DataTracker.Abstractions;
using VRS.DataTracker.Abstractions.Dto.Settings;
using VRS.DataTracker.Abstractions.Dto.Transmission;
using VSR.DataTracker.Infrastructure.Abstractions.Repositories;
using VSR.DataTracker.Infrastructure.Abstractions.Requests;

namespace VRS.DataTracker.Core
{
    internal class TransmissionConsumer : ITransmissionConsumer
    {
        private IServiceScopeFactory serviceScopeFactory;
        private readonly RabbitMQSettings settings;

        public TransmissionConsumer(IServiceScopeFactory serviceScopeFactory, RabbitMQSettings settings)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.settings = settings;
        }

        public void Run()
        {
            try
            {
                Consume();
            }
            catch
            {
                Thread.Sleep(10000);
                Run();
            }
        }

        private void Consume()
        {
            var factory = new ConnectionFactory() { HostName = settings.Host };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: settings.TransmissionQueueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    Console.WriteLine(" [x] Received {0}", Encoding.UTF8.GetString(body));
                    var transmission = JsonSerializer.Deserialize<TransmissionDto>(body);
                    HandleMessage(transmission);
                };

                channel.BasicConsume(queue: settings.TransmissionQueueName,
                                     autoAck: true,
                                     consumer: consumer);
                while (true)
                {
                }
            }
        }

        private void HandleMessage(TransmissionDto transmission)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IVesselRepository>();
                var vesselId = repository.CreateOrUpdateVessel(transmission.IMO, transmission.VesselName);
                var createRecorRequest = new CreateTrackRecordRequest
                {
                    VesselId = vesselId,
                    DateTime = transmission.DateTime,
                    Latitude = transmission.Position.Latitude,
                    Longitude = transmission.Position.Longitude
                };
                repository.CreateTrackDataRecord(createRecorRequest);
            }
        }
    }
}