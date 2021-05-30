using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buildy.Data.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Course> Courses { get; set; }

    }
}
