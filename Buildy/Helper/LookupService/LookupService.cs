using Buildy.Data;
using Buildy.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buildy.Helper.LookupService
{
    public class LookupService : ILookupService
    {
        private readonly ApplicationDbContext _context;

        public LookupService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SelectList> GetNotEnrolledInCourseSelectList(int studentId)
        {
            var student = await _context
                                 .Students.FindAsync(studentId);

            var courses = await _context
                            .Courses
                            .Where(course => course.Students.Contains(student) == false)
                            .Select(course => new LookupVM {
                                Id = course.Id,
                                Name = course.Name
                            })
                            .ToListAsync();

            SelectList courseSelectList = new SelectList(courses, "Id", "Name");

            return courseSelectList;
        }
    }
}
