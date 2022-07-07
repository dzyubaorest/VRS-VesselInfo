using System.ComponentModel.DataAnnotations;

namespace VRS.DataTracker.Web.Models
{
    public class VesselTrackingRecordViewModel
    {
        public int VesselTrackDataRecordId { get; set; }

        public string Name { get; set; }

        public string IMO { get; set; }

        [Display(Name = "Date & Time")]
        public DateTime DateTime { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}
