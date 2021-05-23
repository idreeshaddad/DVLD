using AutoMapper;
using Buildy.Data.Entities;
using Buildy.Models.Students;

namespace Buildy.Automapper
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentVM>().ReverseMap();
        }
    }
}
