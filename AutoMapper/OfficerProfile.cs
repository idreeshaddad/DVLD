using AutoMapper;
using DVLD.Data.Entities;
using DVLD.Models.Officer;

namespace DVLD.AutoMapper
{
    public class OfficerProfile : Profile
    {
        public OfficerProfile()
        {
            CreateMap<Officer, OfficerVM>().ReverseMap();
        }
    }
}
