namespace VSR.DataTracker.Infrastructure.Entities
{
    internal class VesselTrackDataRecord
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public int VesselId { get; set; }
        public virtual Vessel Vessel { get; set; }
    }
}