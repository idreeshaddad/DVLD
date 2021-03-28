using AutoMapper;
using DVLD.Data.Entities;
using DVLD.Models.Tickets;

namespace DVLD.AutoMapper
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketVM>().ReverseMap();
        }
    }
}
