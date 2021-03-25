using DVLD.Models.Driver;
using System.ComponentModel.DataAnnotations;

namespace DVLD.Models.Car
{
    public class CarVM
    {
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

        public DriverVM Driver { get; set; }
    }
}
