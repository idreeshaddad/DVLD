using System.ComponentModel.DataAnnotations;

namespace MB.T.DVLD.Web.Models.PoliceCars
{
    public class PoliceCarVM
    {
        public int Id { get; set; }

        [Display(Name ="Manufacturer")]
        public string Manu { get; set; }

        public string Model { get; set; }
        public string CarNumber { get; set; }
    }
}
