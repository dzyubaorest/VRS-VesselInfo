namespace VRS.DataTracker.Abstractions.Dto
{
    public class VesselDataRecordWIthMetadata
    {
        public int VesselId { get; set; }

        public int VesselTrackDataRecordId { get; set; }

        public string Name { get; set; }

        public string IMO { get; set; }

        public DateTime DateTime { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}