using MB.T.DVLD.Web.Models.Driver;
using MB.T.DVLD.Web.Models.InsurancePolicy;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MB.T.DVLD.Web.Models.Car
{
    public class CarVM
    {
        public CarVM()
        {
            Drivers = new List<DriverVM>();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Manufacturer")]
        public string Manu { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        [Display(Name = "License Plate")]
        public string LicensePlate { get; set; }

        public List<DriverVM> Drivers { get; set; }

        public InsurancePolicyVM InsurancePolicy { get; set; }
    }
}
