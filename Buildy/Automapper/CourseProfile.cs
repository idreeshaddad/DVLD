using AutoMapper;
using Buildy.Data.Entities;
using Buildy.Models.Courses;

namespace Buildy.Automapper
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseVM>().ReverseMap();
        }
    }
}
