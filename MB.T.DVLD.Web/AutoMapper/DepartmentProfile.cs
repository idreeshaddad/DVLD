using AutoMapper;
using MB.T.DVLD.Entities;
using MB.T.DVLD.Web.Models.Departments;

namespace MB.T.DVLD.Web.AutoMapper
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentVM>().ReverseMap();
        }
    }
}
