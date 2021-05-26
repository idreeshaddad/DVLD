using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Buildy.Data.Entities
{
    public class Student
    {
        public Student()
        {
            ExamPapers = new List<ExamPaper>();
            Courses = new List<Course>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public DateTime DateOfBirth { get; set; }

        public List<ExamPaper> ExamPapers  { get; set; }

        public List<Course> Courses { get; set; }
    }
}
