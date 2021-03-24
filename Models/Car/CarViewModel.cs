﻿using System.ComponentModel.DataAnnotations;

namespace DVLD.Models.Car
{
    public class CarViewModel
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

        public string DriverFullName { get; set; }
    }
}
