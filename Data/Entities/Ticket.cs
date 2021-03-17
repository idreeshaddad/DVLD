using System;
using System.ComponentModel.DataAnnotations;


namespace DVLD.Data.Entities
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [Display(Name =  "Issue Date")]
        public DateTime IssueDate { get; set; }


        public int? DriverId { get; set; }
        public Driver Driver { get; set; }

        public int? CarId { get; set; }
        public Car Car { get; set; }


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
