using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DVLD.Data;
using DVLD.Data.Entities;
using DVLD.Models.Officer;
using AutoMapper;

namespace DVLD.Controllers
{
    public class OfficerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OfficerController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Officers
        public async Task<IActionResult> Index()
        {
            List<OfficerViewModel> offiersVM = await _context
                                                    .Officers
                                                    .Select(officer => _mapper.Map<OfficerViewModel>(officer))
                                                    .ToListAsync();

            return View(offiersVM);
        }

        // GET: Officers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officer = await _context.Officers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (officer == null)
            {
                return NotFound();
            }

            return View(officer);
        }

        // GET: Officers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Officers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfficerViewModel OfficerVM)
        {
            if (ModelState.IsValid)
            {
                Officer officer = _mapper.Map<Officer>(OfficerVM);

                _context.Add(officer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(OfficerVM);
        }

        // GET: Officers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officer = await _context.Officers.FindAsync(id);
            if (officer == null)
            {
                return NotFound();
            }
            return View(officer);
        }

        // POST: Officers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,BadgeNumber,Rank")] Officer officer)
        {
            if (id != officer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(officer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfficerExists(officer.Id))
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
            return View(officer);
        }

        // GET: Officers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officer = await _context.Officers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (officer == null)
            {
                return NotFound();
            }

            return View(officer);
        }

        // POST: Officers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var officer = await _context.Officers.FindAsync(id);
            _context.Officers.Remove(officer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfficerExists(int id)
        {
            return _context.Officers.Any(e => e.Id == id);
        }
    }
}
