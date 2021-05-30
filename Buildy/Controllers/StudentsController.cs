using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Buildy.Data;
using Buildy.Data.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Buildy.Models.Students;
using Buildy.Models.Exams;
using Buildy.Models.Courses;
using Buildy.Helper.LookupService;

namespace Buildy.Controllers
{
    public class StudentsController : Controller
    {
        #region Data and Constructors

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentsController> _logger;
        private readonly ILookupService _lookupService;

        public StudentsController(ApplicationDbContext context,
                                  IMapper mapper,
                                  ILogger<StudentsController> logger,
                                  ILookupService lookupService)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _lookupService = lookupService;
        }

        #endregion

        #region Actions

        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync();
            var studentVMs = _mapper.Map<List<Student>, List<StudentVM>>(students);

            return View(studentVMs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context
                                    .Students
                                    .Include(student => student.ExamPapers)
                                    .Include(student => student.Courses)
                                    .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            var studentVM = _mapper.Map<Student, StudentVM>(student);

            return View(studentVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentVM studentVM)
        {
            if (ModelState.IsValid)
            {
                var student = _mapper.Map<StudentVM, Student>(studentVM);

                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(studentVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var studentVM = _mapper.Map<Student, StudentVM>(student);

            return View(studentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentVM studentVM)
        {
            if (id != studentVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var student = _mapper.Map<StudentVM, Student>(studentVM);
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(studentVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(studentVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddExamPaper(int studentId)
        {
            var examPaperVM = new ExamPaperVM();
            examPaperVM.StudentId = studentId;

            examPaperVM.StudentName = await _context
                                        .Students
                                        .Where(student => student.Id == studentId)
                                        .Select(student => student.FullName)
                                        .SingleAsync();

            return View(examPaperVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddExamPaper(ExamPaperVM examPaperVM)
        {
            if (ModelState.IsValid)
            {
                var examPaper = _mapper.Map<ExamPaperVM, ExamPaper>(examPaperVM);

                _context.Update(examPaper);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(examPaperVM);
        }

        public async Task<IActionResult> AddCourse(int studentId)
        {
            var courseVM = new CreateUpdateCourseVM();

            courseVM.StudentId = studentId;

            courseVM.StudentName = await _context
                                        .Students
                                        .Where(student => student.Id == studentId)
                                        .Select(student => student.FullName)
                                        .SingleAsync();

            courseVM.CourseSelectList = await _lookupService.GetNotEnrolledInCourseSelectList(studentId);


            return View(courseVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(CreateUpdateCourseVM courseVM)
        {
            if (ModelState.IsValid)
            {
                var course = await _context.Courses.FindAsync(courseVM.Id);
                var student = await _context.Students.FindAsync(courseVM.StudentId);

                course.Students.Add(student);

                _context.Update(course);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(courseVM);
        }

        #endregion

        #region Private Functions

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        #endregion
    }
}
