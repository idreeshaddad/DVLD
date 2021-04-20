using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace MB.T.DVLD.Web.Models.Tickets
{
    public class CreateEditTicketVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Issue Date")]
        public DateTime IssueDate { get; set; }


        [Required]
        [Display(Name = "Driver")]
        public int DriverId { get; set; }

        [Required]
        [Display(Name = "Car")]
        public int CarId { get; set; }


        [Required]
        public string Location { get; set; }


        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "لا تكتب كثير ولا قليل يا امير", MinimumLength = 10)]
        public string Reason { get; set; }


        public double Amount { get; set; }


        [Required]
        [Display(Name = "Officer")]
        public int OfficerId { get; set; }

        
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public bool Paid { get; set; }
        public DateTime PaymentDate { get; set; }


        public SelectList DriverSelectList { get; set; }
        public SelectList CarSelectList { get; set; }
        public SelectList OfficerSelectList { get; set; }
        public SelectList DepartmentSelectList { get; set; }
    }
}
