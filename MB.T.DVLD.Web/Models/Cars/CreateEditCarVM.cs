using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MB.T.DVLD.Web.Models.Car
{
    public class CreateEditCarVM
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

        [Display(Name = "Drivers")]
        public List<int> DriverIds { get; set; }

        public int? InsurancePolicyId { get; set; }

        public SelectList DriverSelectList { get; set; }
        //public SelectList InsuranceSelectList { get; set; }
    }
}
