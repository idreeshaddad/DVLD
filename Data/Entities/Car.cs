﻿using System.ComponentModel.DataAnnotations;

namespace DVLD.Data.Entities
{
    public class Car
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

        public Driver Driver { get; set; }
    }
}
