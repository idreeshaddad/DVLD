using AutoMapper;
using Buildy.Data.Entities;
using Buildy.Models.Teachers;

namespace Buildy.Automapper
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Teacher, TeacherVM>().ReverseMap();
        }             
    }
}