using AutoMapper;
using MB.T.DVLD.Entities;
using MB.T.DVLD.Web.Models.Tickets;

namespace MB.T.DVLD.Web.AutoMapper
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketVM>().ReverseMap();
            CreateMap<Ticket, CreateEditTicketVM>().ReverseMap();
        }
    }
}
