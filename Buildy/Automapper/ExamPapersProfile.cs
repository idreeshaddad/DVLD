using AutoMapper;
using Buildy.Data.Entities;
using Buildy.Models.Exams;

namespace Buildy.Automapper
{
    public class ExamPapersProfile : Profile
    {
        public ExamPapersProfile()
        {
            CreateMap<ExamPaper, ExamPaperVM>().ReverseMap();
        }
    }
}
