using AutoMapper;
using MB.T.DVLD.Entities;
using MB.T.DVLD.Web.Models.Insurances;

namespace MB.T.DVLD.Web.AutoMapper
{
    public class InsuranceProfile : Profile
    {
        public InsuranceProfile()
        {
            CreateMap<InsuranceCompany, InsuranceVM>();
            CreateMap<InsuranceVM, InsuranceCompany>();
        }
    }
}
