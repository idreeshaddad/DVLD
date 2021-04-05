using MB.T.DVLD.Utilities.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MB.T.DVLD.Web.Models.Insurances
{
    public class InsuranceVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }
        [Display(Name = "Company Name")]
        [Required]
        public string CompanyName { get; set; }
        [Display(Name = "Insurance Type")]
        [Required]
        public InsuranceType InsuranceType { get; set; }
    }
}
