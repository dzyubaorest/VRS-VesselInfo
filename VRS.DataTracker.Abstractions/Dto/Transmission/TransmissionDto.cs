namespace VRS.DataTracker.Abstractions.Dto.Transmission
{
    public class TransmissionDto
    {
        public string IMO { get; set; }
        
        public string VesselName { get; set; }

        public DateTime DateTime { get; set; }

        public GeolocationPosition Position { get; set; }
    }
}