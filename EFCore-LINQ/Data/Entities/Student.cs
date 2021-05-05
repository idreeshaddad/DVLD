using System;
using System.Collections.Generic;

namespace EFCore_LINQ.Data.Entities
{
    public class Student
    {
        public Student()
        {
            Courses = new List<Course>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Course> Courses { get; set; }
    }
}
