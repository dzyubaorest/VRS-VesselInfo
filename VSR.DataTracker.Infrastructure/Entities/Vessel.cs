namespace VSR.DataTracker.Infrastructure.Entities
{
    internal class Vessel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IMO { get; set; }


        public virtual ICollection<VesselTrackDataRecord> TrackDataRecords { get; set; }
    }
}