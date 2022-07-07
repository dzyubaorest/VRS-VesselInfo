using VRS.DataTracker.Abstractions;
using VRS.DataTracker.Abstractions.Dto.Settings;
using VRS.DataTracker.Abstractions.Dto.Transmission;

namespace VRS.DataTracker.Core
{
    internal class TransmissionDataMessagePublisher : RabbitMQMessagePublisherBase<TransmissionDto>, ITransmissionDataMessagePublisher
    {
        public TransmissionDataMessagePublisher(RabbitMQSettings settings) :
            base(settings, settings.TransmissionQueueName)
        {
        }

        public Task PublishAsync(TransmissionDto data)
        {
            Publish(data);
            return Task.CompletedTask;
        }
    }
}