using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DVLD.Data;
using DVLD.Data.Entities;
using DVLD.Models.Officer;
using AutoMapper;

namespace DVLD.Controllers
{
    public class OfficerController : Controller
    {
        #region Data and Constructors 

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OfficerController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        public async Task<IActionResult> Index()
        {
            List<Officer> officer = await _context
                                            .Officers
                                            .ToListAsync();

            List<OfficerVM> officerVMs = _mapper.Map<List<Officer>, List<OfficerVM>>(officer);

            return View(officerVMs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Officer officer = await _context
                                        .Officers
                                        .FirstOrDefaultAsync(m => m.Id == id);

            if (officer == null)
            {
                return NotFound();
            }

            OfficerVM officerVM = _mapper.Map<Officer, OfficerVM>(officer);

            return View(officerVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfficerVM OfficerVM)
        {
            if (ModelState.IsValid)
            {
                Officer officer = _mapper.Map<OfficerVM, Officer>(OfficerVM);

                _context.Add(officer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(OfficerVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Officer officer = await _context.Officers.FindAsync(id);

            if (officer == null)
            {
                return NotFound();
            }

            OfficerVM officerVM = _mapper.Map<Officer, OfficerVM>(officer);

            return View(officerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OfficerVM OfficerVM)
        {
            if (id != OfficerVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Officer officer = _mapper.Map<OfficerVM, Officer>(OfficerVM);

                    _context.Update(officer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfficerExists(OfficerVM.Id))
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

            return View(OfficerVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officer = await _context
                                    .Officers
                                    .FirstOrDefaultAsync(m => m.Id == id);

            if (officer == null)
            {
                return NotFound();
            }

            OfficerVM officerVM = _mapper.Map<Officer, OfficerVM>(officer);

            return View(officerVM);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var officer = await _context.Officers.FindAsync(id);
            _context.Officers.Remove(officer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool OfficerExists(int id)
        {
            return _context.Officers.Any(e => e.Id == id);
        }

        #endregion
    }
}
