using MB.T.DVLD.Utilities.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace MB.T.DVLD.Web.Models.Cars
{
    public class CarPolicyVM
    {
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "Expiry Time")]
        public DateTime ExpiryDate { get; set; }

        [Required]
        [Display(Name = "Insurance Type")]
        public InsuranceType InsuranceType { get; set; }

        [Required]
        [Display(Name = "Insurance Company")]
        public int InsuranceCompanyId { get; set; }

        public SelectList CompanySelectList { get; set; }
    }
}
