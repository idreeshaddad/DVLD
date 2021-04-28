using System.ComponentModel.DataAnnotations;

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
