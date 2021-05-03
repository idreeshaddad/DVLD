using System;
using System.Collections.Generic;

namespace EFCore_LINQ.Data.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Course> Courses { get; set; }
    }
}
