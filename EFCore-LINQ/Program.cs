using EFCore_LINQ.Data;
using EFCore_LINQ.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

            // TODO change Student(N) - Course(N)
            //CreateTwoCourses_AddTwoStudentsToAllCourses();

            //PrintStudentsAndTheirCourses();

            //DoesAStudentWithNameAnasExist();

            PrintAllStudentsThatStartWithSameer();
        }

        private static void PrintAllStudentsThatStartWithSameer()
        {
            Console.Write("Enter name to search for: ");
            var keyWord = Console.ReadLine().Trim();

            using (var context = new SchoolContext())
            {
                var students = context
                                .Students
                                .Where(student => student.FirstName.Contains(keyWord))
                                .ToList();

                foreach (var student in students)
                {
                    Console.WriteLine($"Student Name: {student.FirstName}.");
                }

            }
        }

        private static void DoesAStudentWithNameAnasExist()
        {
            using (var context = new SchoolContext())
            {
                var doesAnasExist = context.Students.Any(student => student.FirstName == "anas");


                if (doesAnasExist)
                {
                    Console.WriteLine("Anas Exists");
                }
                else
                {
                    Console.WriteLine("No Anas Here");
                }
            }
        }

        private static void PrintStudentsAndTheirCourses()
        {
            List<Student> students = new List<Student>();

            using (var context = new SchoolContext())
            {
                students = context
                                    .Students
                                    .Include(stu => stu.Courses)
                                    .ToList();
            }

            foreach (var student in students)
            {
                Console.Write($"Student: {student.FirstName}, Course: ");

                if (student.Courses.Count > 0)
                {
                    for (int i = 0; i < student.Courses.Count(); i++)
                    {
                        Console.Write($"{student.Courses[i].Name}");

                        // Show the comma after every course name BUT the last one
                        if (i + 1 < student.Courses.Count)
                        {
                            Console.Write(", ");
                        }
                    }
                }
                else
                {
                    Console.Write("Student is not enrolled in any courses.");
                }

                Console.WriteLine(Environment.NewLine);
            }
        }

        private static void CreateTwoCourses_AddTwoStudentsToAllCourses()
        {
            var courseMaths = new Course()
            {
                Name = "Maths",
                Code = "104"
            };
            var courseBiology = new Course()
            {
                Name = "Biology",
                Code = "852"
            };

            var stuAnasJaffal = new Student()
            {
                FirstName = "Anas Jaffal",
                DateOfBirth = new DateTime(1998, 8, 17),
            };
            stuAnasJaffal.Courses.Add(courseMaths);
            stuAnasJaffal.Courses.Add(courseBiology);

            var stuHamzaXaixo = new Student()
            {
                FirstName = "Hamza Xaixo",
                DateOfBirth = new DateTime(1998, 8, 17)
            };
            stuHamzaXaixo.Courses.Add(courseMaths);
            stuHamzaXaixo.Courses.Add(courseBiology);

            using (var context = new SchoolContext())
            {
                context.Students.Add(stuAnasJaffal);
                context.Students.Add(stuHamzaXaixo);
                context.SaveChanges();
            }

        }

        private static void CreateMathCourse_WithStudentId()
        {
            using (var context = new SchoolContext())
            {
                var mathCourse = new Course()
                {
                    Name = "Maths for Kids",
                    Code = "258962"
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
                                    .Where(stu => stu.FirstName == "Anas")
                                    .Single();

                var mathCourse = new Course()
                {
                    Name = "Maths for Kids",
                    Code = "258962"
                };

                context.Courses.Add(mathCourse);
                context.SaveChanges();
            }
        }

        private static void CreateStudentAnas()
        {
            var stu = new Student()
            {
                FirstName = "Anas",
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
