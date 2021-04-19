using System;
using MB.T.DVLD.Utilities.Enums;

namespace MB.T.DVLD.Entities
{
    public class InsurancePolicy
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ExpiryDate { get; set; }
        public InsuranceType InsuranceType { get; set; }

        public int? InsuranceCompanyId { get; set; }
        public InsuranceCompany InsuranceCompany { get; set; }
    }
}
