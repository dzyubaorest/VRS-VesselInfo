using Microsoft.AspNetCore.Mvc;
using VRS.DataTracker.Abstractions;
using VRS.DataTracker.Abstractions.Dto.Transmission;

namespace VRS.DataTracker.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransmissionController : ControllerBase
    {
        private readonly ITransmissionDataMessagePublisher messagePublisher;

        public TransmissionController(ITransmissionDataMessagePublisher messagePublisher)
        {
            this.messagePublisher = messagePublisher;
        }

        [HttpPost]
        public async Task Post(TransmissionDto data)
        {
            // all actual validation of data should be processed later, by message consumer.
            // In API layer all we want is just to push message (we want to handle possible high load)
            if (data != null)
            {
                await messagePublisher.PublishAsync(data);
            }
        }
    }
}