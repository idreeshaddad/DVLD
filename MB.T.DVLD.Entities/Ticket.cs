using System;

namespace MB.T.DVLD.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public DateTime IssueDate { get; set; }

        public int DriverId { get; set; }
        public Driver Driver { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }

        public string Location { get; set; }
        public string Reason { get; set; }
        public double Amount { get; set; }


        public int OfficerId { get; set; }
        public Officer Officer { get; set; }


        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        public bool Paid  {get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
