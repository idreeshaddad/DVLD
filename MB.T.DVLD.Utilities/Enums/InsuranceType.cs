using System.ComponentModel.DataAnnotations;

namespace MB.T.DVLD.Utilities.Enums
{
    public enum InsuranceType
    {
        [Display(Name = "Third-Party")]
        ThirdParty = 0,
        [Display(Name = "Third-Party,Fire,Theft")]
        ThirdPartyFireTheft = 1,
        [Display(Name = "Fully Comprehensive")]
        FullyComprehensive = 2
    }
}
