using DVLD.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVLD.Data.Entities
{
    public class Driver
    {
        public int Id { get; set; }


        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [NotMapped]
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
