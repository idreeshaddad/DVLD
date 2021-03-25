using System;
using System.ComponentModel.DataAnnotations;

namespace DVLD.Models.Tickets
{
    public class TicketVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Issue Date")]
        public DateTime IssueDate { get; set; }


        public int? DriverId { get; set; }
        public string DriverFullName { get; set; }

        public int? CarId { get; set; }
        public string LicensePlate { get; set; }
        public string CarModel { get; set; }


        [Required]
        public string Location { get; set; }


        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "لا تكتب كثير ولا قليل يا امير", MinimumLength = 10)]
        public string Reason { get; set; }


        public double Amount { get; set; }


        [Required]
        [Display(Name = "Officer Name")]
        public string OfficerName { get; set; }
    }
}
