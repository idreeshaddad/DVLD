using MB.T.DVLD.Utilities.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace MB.T.DVLD.Web.Models.InsurancePolicies
{
    public class CreateEditInsurancePolicyVM
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public InsuranceType InsuranceType { get; set; }

        [Required]
        public int InsuranceCompanyId { get; set; }

        public SelectList CompanySelectList { get; set; }

        //public int CarId { get; set; }
    }
}
