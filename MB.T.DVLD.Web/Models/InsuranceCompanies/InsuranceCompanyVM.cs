using System;
using System.Collections.Generic;
using System.Linq;
using MB.T.DVLD.Entities;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MB.T.DVLD.Web.Models.InsuranceCompanies
{
    public class InsuranceCompanyVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Required]
        public string Slogan { get; set; }
    }
}
