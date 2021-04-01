using AutoMapper;
using MB.T.DVLD.Entities;
using MB.T.DVLD.Web.Models.Driver;

namespace MB.T.DVLD.Web.AutoMapper
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<Driver, DriverVM>().ReverseMap();
        }
    }
}
