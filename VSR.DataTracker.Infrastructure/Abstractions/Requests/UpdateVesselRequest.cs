namespace VSR.DataTracker.Infrastructure.Abstractions.Requests
{
    public class UpdateVesselRequest
    {
        public int VesselId { get; set; }

        public string IMO { get; set; }

        public string Name { get; set; }
    }
}