using System;
using DVLD.Enums;
using System.ComponentModel.DataAnnotations;

namespace DVLD.Models.Driver
{
    public class DriverVM
    {
        public int Id { get; set; }


        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [Required]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }


        [Required]
        [Display(Name = "License Number")]
        public string LicenseNumber { get; set; }


        [Required]
        [Display(Name = "License Type")]
        public LicenseType LicenseType { get; set; }


        [Required]
        public Gender Gender { get; set; }
    }
}
