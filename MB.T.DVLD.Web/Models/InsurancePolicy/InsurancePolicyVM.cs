using MB.T.DVLD.Entities;
using MB.T.DVLD.Utilities.Enums;
using MB.T.DVLD.Web.Models.InsuranceCompanies;
using System;
using System.ComponentModel.DataAnnotations;

namespace MB.T.DVLD.Web.Models.InsurancePolicy
{
    public class InsurancePolicyVM
    {
        public int Id { get; set; }

        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }

        [Display(Name = "Insurance Type")]
        public InsuranceType InsuranceType { get; set; }

        public int? InsuranceCompanyId { get; set; }

        public InsuranceCompanyVM InsuranceCompany { get; set; }
    }
}
