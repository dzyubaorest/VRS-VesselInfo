using VRS.DataTracker.Abstractions.Dto;
using VSR.DataTracker.Infrastructure.Abstractions.Requests;

namespace VSR.DataTracker.Infrastructure.Abstractions.Repositories
{
    public interface IVesselRepository
    {
        Task<IReadOnlyCollection<VesselDto>> GetVesselsAsync();

        Task<VesselDataRecordWIthMetadata> GetVesselDataRecordAsync(int vesselTrackDataRecordId);

        Task UpdateTrackDataRecordAsync(UpdateTrackRecordRequest updateRequest);

        Task UpdateVesselAsync(UpdateVesselRequest updateRequest);

        int CreateOrUpdateVessel(string imo, string name);

        void CreateTrackDataRecord(CreateTrackRecordRequest createRequest);
    }
}