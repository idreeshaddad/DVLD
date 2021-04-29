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

            Console.Write("Enter the employee file ID you want to get? ");
            var inputEmpId = Convert.ToInt32(Console.ReadLine());

            var emp = employees.Where(emp => emp.Id == inputEmpId).SingleOrDefault();

            if (emp != null)
            {
                Console.WriteLine($"Id: {emp.Id}, Name: {emp.FirstName} {emp.LastName}, DOB: {emp.DateOfBirth}");
            }
            else
            {
                Console.WriteLine($"No employee file with id {inputEmpId} is found!");
            }
        }









        private static List<Employee> GetListOfEmployees()
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
    }
}
