using EFCore_LINQ.Data;
using EFCore_LINQ.Data.Entities;
using System;

namespace EFCore_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SchoolContext())
            {
                var stu = new Student()
                {
                    Name = "Anas"
                };

                context.Students.Add(stu);
                context.SaveChanges();
            }
        }
    }
}
