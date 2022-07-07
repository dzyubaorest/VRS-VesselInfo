using Microsoft.AspNetCore.Mvc.RazorPages;
using VRS.DataTracker.Web.Models;
using VSR.DataTracker.Infrastructure.Abstractions.Repositories;

namespace VRS.DataTracker.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IVesselRepository repository;

        public IndexModel(IVesselRepository repository)
        {
            this.repository = repository;
        }

        public IList<VesselTrackingRecordViewModel> Vessels { get; set; }

        public async Task OnGetAsync()
        {
            var vessels = await repository.GetVesselsAsync();
            Vessels = vessels.SelectMany(v => v.TrackDataRecords.Select(r => new VesselTrackingRecordViewModel
            {
                VesselTrackDataRecordId = r.Id,
                Latitude = r.Latitude,
                Longitude = r.Longitude,
                DateTime = r.DateTime,
                IMO = v.IMO,
                Name = v.Name
            })).ToList();
        }
    }
}