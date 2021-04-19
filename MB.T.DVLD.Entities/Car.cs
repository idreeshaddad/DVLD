using System.Collections.Generic;

namespace MB.T.DVLD.Entities
{
    public class Car
    {
        public Car()
        {
            Drivers = new List<Driver>();
        }

        public int Id { get; set; }
        public string Manu { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public List<Driver> Drivers { get; set; }

        public int? InsurancePolicyId { get; set; }
        public virtual InsurancePolicy InsurancePolicy { get; set; }
    }
}
