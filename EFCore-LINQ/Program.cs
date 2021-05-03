using EFCore_LINQ.Data;
using EFCore_LINQ.Data.Entities;
using System;
using System.Linq;

namespace EFCore_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateStudentAnas();
            //CreateMathCourse_WithStudentEntity();
            //CreateMathCourse_WithStudentId();
            
            // TODO change Student(N) - Course (N)
        }

        private static void CreateMathCourse_WithStudentId()
        {
            using (var context = new SchoolContext())
            {
                var mathCourse = new Course()
                {
                    Name = "Maths for Kids",
                    Code = "258962",
                    StudentId = 1
                };

                context.Courses.Add(mathCourse);
                context.SaveChanges();
            }
        }

        private static void CreateMathCourse_WithStudentEntity()
        {
            using (var context = new SchoolContext())
            {
                var studentAnas = context
                                    .Students
                                    .Where(stu => stu.Name == "Anas")
                                    .Single();

                var mathCourse = new Course()
                {
                    Name = "Maths for Kids",
                    Code = "258962",
                    Student = studentAnas
                };

                context.Courses.Add(mathCourse);
                context.SaveChanges();
            }
        }

        private static void CreateStudentAnas()
        {
            var stu = new Student()
            {
                Name = "Anas",
                DateOfBirth = new DateTime(2007, 9, 16)
            };

            using (var context = new SchoolContext())
            {
                context.Students.Add(stu);
                context.SaveChanges();
            }
        }
    }
}
