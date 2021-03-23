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

            List<OfficerViewModel> officerVMs = _mapper.Map<List<Officer>, List<OfficerViewModel>>(officer);

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

            OfficerViewModel officerVM = _mapper.Map<Officer, OfficerViewModel>(officer);

            return View(officerVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfficerViewModel OfficerVM)
        {
            if (ModelState.IsValid)
            {
                Officer officer = _mapper.Map<OfficerViewModel, Officer>(OfficerVM);

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

            OfficerViewModel officerVM = _mapper.Map<Officer, OfficerViewModel>(officer);

            return View(officerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OfficerViewModel OfficerVM)
        {
            if (id != OfficerVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Officer officer = _mapper.Map<OfficerViewModel, Officer>(OfficerVM);

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

            OfficerViewModel officerVM = _mapper.Map<Officer, OfficerViewModel>(officer);

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
