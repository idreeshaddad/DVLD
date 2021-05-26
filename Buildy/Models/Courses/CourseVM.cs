using Microsoft.AspNetCore.Mvc.Rendering;

namespace Buildy.Models.Courses
{
    public class CourseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public SelectList CourseSelectList { get; set; }
    }
}
