using VRS.DataTracker.Abstractions.Dto.Transmission;

namespace VRS.DataTracker.Abstractions
{
    public interface ITransmissionDataMessagePublisher
    {
        Task PublishAsync(TransmissionDto data);
    }
}