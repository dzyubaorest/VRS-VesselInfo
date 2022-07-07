namespace VSR.DataTracker.Infrastructure.Abstractions.Requests
{
    public class CreateVesselRequest
    {
        public string IMO { get; set; }

        public string Name { get; set; }
    }
}