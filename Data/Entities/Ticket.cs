using System;

namespace DVLD.Data.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public DateTime IssueDate { get; set; }
        public string VehicleOwnerName { get; set; }
        public string VehicleModel { get; set; }
        public string LicensePlate { get; set; }
        public string Location { get; set; }
        public string Reason { get; set; }
        public double Amount { get; set; }
        public string OfficerName { get; set; }
    }
}
