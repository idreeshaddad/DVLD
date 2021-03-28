using DVLD.Models.Car;
using DVLD.Models.Driver;
using DVLD.Models.Officer;
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


        public DriverVM Driver { get; set; }

        public CarVM Car { get; set; }


        [Required]
        public string Location { get; set; }


        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "لا تكتب كثير ولا قليل يا امير", MinimumLength = 10)]
        public string Reason { get; set; }


        public double Amount { get; set; }


        [Required]
        public OfficerVM Officer { get; set; }
    }
}
