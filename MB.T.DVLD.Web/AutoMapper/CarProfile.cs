using AutoMapper;
using MB.T.DVLD.Entities;
using MB.T.DVLD.Web.Models.Car;
using System.Linq;

namespace MB.T.DVLD.Web.AutoMapper
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarVM>();

            CreateMap<CreateEditCarVM, Car>();

            CreateMap<Car, CreateEditCarVM>()
                .ForMember(dest => dest.DriverIds, opts => opts.MapFrom(src => src.Drivers.Select(driver => driver.Id)));
        }
    }
}
