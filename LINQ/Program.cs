using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            var zarqaEmployees = GetZarqaEmployees().Where(emp => emp.Id > 45).ToList();

            zarqaEmployees.ForEach(emp => {
                Console.WriteLine(emp.FirstName);
            });
        }


        private static List<Employee> GetZarqaEmployees()
        {
            List<Employee> employees = new List<Employee>();

            employees.Add(new Employee()
            {
                Id = 45,
                FirstName = "Ali",
                LastName = "Al-Akhdar",
                DateOfBirth = new DateTime(2004, 8, 12)
            });

            employees.Add(new Employee()
            {
                Id = 107,
                FirstName = "Mohammad",
                LastName = "Saleem",
                DateOfBirth = new DateTime(2005, 2, 17)
            });

            employees.Add(new Employee()
            {
                Id = 123,
                FirstName = "Anas",
                LastName = "Blue Cheese",
                DateOfBirth = new DateTime(2014, 12, 31)
            });

            employees.Add(new Employee()
            {
                Id = 22,
                FirstName = "Sameer",
                LastName = "Abu Laila",
                DateOfBirth = new DateTime(1979, 7, 19)
            });

            return employees;
        }

        private static List<Employee> GetNewYorkEmployees()
        {
            List<Employee> employees = new List<Employee>();

            employees.Add(new Employee()
            {
                Id = 45,
                FirstName = "Micheal",
                LastName = "Jackson",
                DateOfBirth = new DateTime(1971, 8, 12)
            });

            employees.Add(new Employee()
            {
                Id = 107,
                FirstName = "Adam",
                LastName = "Zamitoni",
                DateOfBirth = new DateTime(2005, 2, 17)
            });

            employees.Add(new Employee()
            {
                Id = 123,
                FirstName = "Young",
                LastName = "Wii",
                DateOfBirth = new DateTime(2014, 12, 31)
            });

            employees.Add(new Employee()
            {
                Id = 22,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1979, 7, 19)
            });

            return employees;
        }
    }
}
