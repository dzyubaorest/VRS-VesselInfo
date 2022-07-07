namespace VSR.DataTracker.Infrastructure.Abstractions.Requests
{
    public class CreateTrackRecordRequest
    {
        public int VesselId { get; set; }

        public DateTime DateTime { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}