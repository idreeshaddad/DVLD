using AutoMapper;
using DVLD.Data.Entities;
using DVLD.Models.Driver;

namespace DVLD.AutoMapper
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<Driver, DriverVM>().ReverseMap();
        }
    }
}
