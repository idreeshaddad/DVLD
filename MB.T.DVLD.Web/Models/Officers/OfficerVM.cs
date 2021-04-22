using MB.T.DVLD.Entities;
using MB.T.DVLD.Utilities.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MB.T.DVLD.Web.Models.Officer
{
    public class OfficerVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Officer Full Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [Required]
        [Display(Name = "Badge Number")]
        public string BadgeNumber { get; set; }

        public Rank Rank { get; set; }


        public int? DepartmentId { get; set; }

        [Display(Name = "Department")]
        public Department Department { get; set; }


        public SelectList DepartmentSelectList { get; set; }

        public SelectList PoliceCarSelectList { get; set; }
    }
}
