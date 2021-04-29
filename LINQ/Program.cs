using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            var employees = GetListOfEmployees();

            var filteredEmployees = employees.Where(emp => emp.Id > 1);

            foreach (var emp in filteredEmployees)
            {
                Console.WriteLine($"Id: {emp.Id}, Name: {emp.FirstName} {emp.LastName}, DOB: {emp.DateOfBirth}");
            }

            Console.WriteLine($"The count of employees list is: {employees.Count()}");

            Console.WriteLine($"The count of filteredEmployees list is: {filteredEmployees.Count()}");
        }









        private static List<Employee> GetListOfEmployees()
        {
            List<Employee> employees = new List<Employee>();

            employees.Add(new Employee()
            {
                Id = 1,
                FirstName = "Ali",
                LastName = "Al-Akhdar",
                DateOfBirth = new DateTime(2004, 8, 12)
            });

            employees.Add(new Employee()
            {
                Id = 2,
                FirstName = "Mohammad",
                LastName = "Saleem",
                DateOfBirth = new DateTime(2005, 2, 17)
            });

            employees.Add(new Employee()
            {
                Id = 3,
                FirstName = "Anas",
                LastName = "Blue Cheese",
                DateOfBirth = new DateTime(2014, 12, 31)
            });

            employees.Add(new Employee()
            {
                Id = 4,
                FirstName = "Sameer",
                LastName = "Abu Laila",
                DateOfBirth = new DateTime(1979, 7, 19)
            });

            return employees;
        }
    }
}
