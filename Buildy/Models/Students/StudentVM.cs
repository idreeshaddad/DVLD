using System;
using System.ComponentModel.DataAnnotations;

namespace Buildy.Models.Students
{
    public class StudentVM
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(12)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string Fullname
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
    }
}
