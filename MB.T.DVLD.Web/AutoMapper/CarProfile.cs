using AutoMapper;
using MB.T.DVLD.Entities;
using MB.T.DVLD.Web.Models.Car;

namespace MB.T.DVLD.Web.AutoMapper
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarVM>();
            CreateMap<CreateEditCarVM, Car>().ReverseMap();
        }
    }
}
