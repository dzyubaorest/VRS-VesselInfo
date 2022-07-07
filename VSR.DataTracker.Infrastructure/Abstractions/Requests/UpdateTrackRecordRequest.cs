namespace VSR.DataTracker.Infrastructure.Abstractions.Requests
{
    public class UpdateTrackRecordRequest
    {
        public int Id { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}