using AutoMapper;
using DVLD.Data.Entities;
using DVLD.Models.Car;

namespace DVLD.AutoMapper
{
    public class CarProfile: Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarViewModel>().ReverseMap();
        }
    }
}
