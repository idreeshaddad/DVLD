using MB.T.DVLD.Entities;
using MB.T.DVLD.Utilities.Enums;
using System;

namespace MB.T.DVLD.Web.Models.InsurancePolicy
{
    public class InsurancePolicyVM
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ExpiryDate { get; set; }
        public InsuranceType InsuranceType { get; set; }

        public int? InsuranceCompanyId { get; set; }
        public InsuranceCompany InsuranceCompany { get; set; }
    }
}
