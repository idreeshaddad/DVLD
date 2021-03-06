using AutoMapper;
using MB.T.DVLD.Entities;
using MB.T.DVLD.Web.Models.InsuranceCompanies;
using MB.T.DVLD.Web.Models.InsurancePolicy;

namespace MB.T.DVLD.Web.AutoMapper
{
    public class InsuranceProfile : Profile
    {
        public InsuranceProfile()
        {
            CreateMap<InsuranceCompany, InsuranceCompanyVM>();
            CreateMap<InsuranceCompanyVM, InsuranceCompany>();
            CreateMap<InsurancePolicy, InsurancePolicyVM>();
        }
    }
}
