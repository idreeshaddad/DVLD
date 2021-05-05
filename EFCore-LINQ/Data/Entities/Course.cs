using System.Collections.Generic;

namespace EFCore_LINQ.Data.Entities
{
    public class Course
    {
        public Course()
        {
            Students = new List<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<Student> Students { get; set; }
    }
}
