using MB.T.DVLD.Utilities.Enums;
using System;
using System.Collections.Generic;

namespace MB.T.DVLD.Entities
{
    public class Driver
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LicenseNumber { get; set; }
        public LicenseType LicenseType { get; set; }
        public Gender Gender { get; set; }
        public List<Car> Cars { get; set; }
    }
}
