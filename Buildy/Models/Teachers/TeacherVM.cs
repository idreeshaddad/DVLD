using Buildy.Data.Entities;
using System.Collections.Generic;

namespace Buildy.Models.Teachers
{
    public class TeacherVM
    {
        public TeacherVM()
        {
            Courses = new List<Course>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Course> Courses { get; set; }
    }
}
