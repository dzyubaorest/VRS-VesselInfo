namespace VRS.DataTracker.Abstractions.Dto
{
    public class VesselTrackDataRecordDto
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}