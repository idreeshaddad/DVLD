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

namespace Buildy.Controllers
{
    public class StudentsController : Controller
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(ApplicationDbContext context,
                                  IMapper mapper,
                                  ILogger<StudentsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        #endregion

        #region Services

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

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
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

        #endregion

        #region Private Functions

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        #endregion
    }
}
