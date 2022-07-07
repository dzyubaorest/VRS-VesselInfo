namespace VRS.DataTracker.Abstractions.Dto
{
    public class VesselDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IMO { get; set; }


        public IReadOnlyCollection<VesselTrackDataRecordDto> TrackDataRecords { get; set; }
    }
}