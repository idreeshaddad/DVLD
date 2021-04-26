using AutoMapper;
using MB.T.DVLD.Entities;
using MB.T.DVLD.Web.Models.PoliceCars;

namespace MB.T.DVLD.Web.AutoMapper
{
    public class PoliceCarProfile : Profile
    {
        public PoliceCarProfile()
        {
            CreateMap<PoliceCar, PoliceCarVM>();
        }
    }
}
