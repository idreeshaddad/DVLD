using MB.T.DVLD.Web.Models.Car;
using MB.T.DVLD.Web.Models.Departments;
using MB.T.DVLD.Web.Models.Driver;
using MB.T.DVLD.Web.Models.Officer;
using System;
using System.ComponentModel.DataAnnotations;

namespace MB.T.DVLD.Web.Models.Tickets
{
    public class TicketVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Issue Date")]
        public DateTime IssueDate { get; set; }


        [Display(Name = "Driver")]
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
        
        public DepartmentVM Department{ get; set; }
        public bool Paid { get; set; }
        public DateTime PaymentDate { get; set; }

    }
}
