using Microsoft.EntityFrameworkCore;
using VRS.DataTracker.Abstractions.Dto;
using VSR.DataTracker.Infrastructure.Abstractions.Repositories;
using VSR.DataTracker.Infrastructure.Abstractions.Requests;
using VSR.DataTracker.Infrastructure.Entities;

namespace VSR.DataTracker.Infrastructure.Repositories
{
    internal class VesselRepository : IVesselRepository
    {
        private readonly VesselDataTrackerDbContext dbContext;   
        public VesselRepository(VesselDataTrackerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<VesselDto>> GetVesselsAsync()
        {
            var vessels = await dbContext.Vessels.Include(v => v.TrackDataRecords).ToArrayAsync();
            return vessels.Select(v => new VesselDto
            {
                Id = v.Id,
                IMO = v.IMO,
                Name = v.Name,
                TrackDataRecords = v.TrackDataRecords.Select(r => new VesselTrackDataRecordDto { 
                    Id = r.Id,
                    DateTime = r.DateTime,
                    Latitude = r.Latitude,
                    Longitude = r.Longitude
                }).ToArray()
            }).ToArray();
        }

        public async Task<VesselDataRecordWIthMetadata> GetVesselDataRecordAsync(int vesselTrackDataRecordId)
        {
            var record = await dbContext.VesselTrackDataRecords.Include(r => r.Vessel).FirstAsync(r => r.Id == vesselTrackDataRecordId);
            return new VesselDataRecordWIthMetadata
            {
                DateTime = record.DateTime,
                Latitude = record.Latitude,
                Longitude = record.Longitude,
                VesselTrackDataRecordId = record.Id,
                VesselId = record.VesselId,
                IMO = record.Vessel.IMO,
                Name = record.Vessel.Name
            };
        }

        public async Task UpdateTrackDataRecordAsync(UpdateTrackRecordRequest updateRequest)
        {
            var trackRecord = await dbContext.VesselTrackDataRecords.FindAsync(updateRequest.Id);

            // from my perspective it makes sense only to update position

            trackRecord.Longitude = updateRequest.Longitude;
            trackRecord.Latitude = updateRequest.Latitude;

            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateVesselAsync(UpdateVesselRequest updateRequest)
        {
            var vessel = await dbContext.Vessels.FindAsync(updateRequest.VesselId);
            vessel.IMO = updateRequest.IMO;
            vessel.Name = updateRequest.Name;

            await dbContext.SaveChangesAsync();
        }

        public int CreateOrUpdateVessel(string imo, string name)
        {
            var vessel = dbContext.Vessels.FirstOrDefault(v => v.IMO == imo || v.Name == name);
            if (vessel == null)
            {
                vessel = new Vessel();
                dbContext.Vessels.Add(vessel);
            }

            vessel.IMO = imo;
            vessel.Name = name;

            dbContext.SaveChanges();
            return vessel.Id;
        }

        public void CreateTrackDataRecord(CreateTrackRecordRequest createRequest)
        {
            var trackRecord = new VesselTrackDataRecord
            {
                DateTime = createRequest.DateTime,
                Latitude = createRequest.Latitude,
                Longitude = createRequest.Longitude,
                VesselId = createRequest.VesselId
            };

            dbContext.VesselTrackDataRecords.Add(trackRecord);

            dbContext.SaveChanges();
        }
    }
}