using AutoMapper;
using DVLD.Data.Entities;
using DVLD.Models.Tickets;

namespace DVLD.AutoMapper
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketVM>()
                .ForMember(dest => dest.LicensePlate, opts => opts.MapFrom(src => src.Car.LicensePlate))
                .ReverseMap();
        }
    }
}
