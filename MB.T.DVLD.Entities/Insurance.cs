using MB.T.DVLD.Utilities.Enums;
using System;

namespace MB.T.DVLD.Entities
{
    public class Insurance
    {
        public int Id { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CompanyName { get; set; }
        public InsuranceType InsuranceType { get; set; }
        
    }
}
