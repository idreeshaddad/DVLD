using System.Collections.Generic;

namespace Buildy.Data.Entities
{
    public class Course
    {
        public Course()
        {
            Students = new List<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public List<Student> Students { get; set; }
    }
}
