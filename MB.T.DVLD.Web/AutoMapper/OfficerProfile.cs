using AutoMapper;
using MB.T.DVLD.Entities;
using MB.T.DVLD.Web.Models.Officer;

namespace MB.T.DVLD.Web.AutoMapper
{
    public class OfficerProfile : Profile
    {
        public OfficerProfile()
        {
            CreateMap<Officer, OfficerVM>().ReverseMap();
        }
    }
}
