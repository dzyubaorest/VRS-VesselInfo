using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VRS.DataTracker.Web.Models;
using VSR.DataTracker.Infrastructure.Abstractions.Repositories;
using VSR.DataTracker.Infrastructure.Abstractions.Requests;

namespace VRS.DataTracker.Web.Pages
{
    public class EditModel : PageModel
    {
        private readonly IVesselRepository repository;

        public EditModel(IVesselRepository repository)
        {
            this.repository = repository;
        }

        [BindProperty]
        public EditVesselTrackingRecordViewModel VesselTrackingRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var data = await repository.GetVesselDataRecordAsync(id.Value);
            VesselTrackingRecord = new EditVesselTrackingRecordViewModel
            {
                VesselTrackDataRecordId = data.VesselTrackDataRecordId,
                VesselId = data.VesselId,
                Name = data.Name,
                DateTime = data.DateTime,
                IMO = data.IMO,
                Latitude = data.Latitude,
                Longitude = data.Longitude
            };


            if (VesselTrackingRecord == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var updateVesselRequest = new UpdateVesselRequest
                {
                    VesselId = VesselTrackingRecord.VesselId,
                    IMO = VesselTrackingRecord.IMO,
                    Name = VesselTrackingRecord.Name
                };
                await repository.UpdateVesselAsync(updateVesselRequest);


                var updateTrackRecordRequest = new UpdateTrackRecordRequest
                {
                    Id = VesselTrackingRecord.VesselTrackDataRecordId,
                    Latitude = VesselTrackingRecord.Latitude,
                    Longitude = VesselTrackingRecord.Longitude
                };
                await repository.UpdateTrackDataRecordAsync(updateTrackRecordRequest);
            }
            catch
            {
                // log error
            }

            return RedirectToPage("./Index");
        }
    }
}