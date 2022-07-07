using Microsoft.EntityFrameworkCore;
using VSR.DataTracker.Infrastructure.Entities;

namespace VSR.DataTracker.Infrastructure
{
    internal class VesselDataTrackerDbContext : DbContext
    {
        public VesselDataTrackerDbContext(DbContextOptions<VesselDataTrackerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vessel> Vessels { get; set; }
        public DbSet<VesselTrackDataRecord> VesselTrackDataRecords { get; set; }
    }
}