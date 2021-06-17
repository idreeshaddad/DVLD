using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MB.T.DVLD.Web.Data;
using MB.T.DVLD.Web.Models.Officer;
using MB.T.DVLD.Entities;
using MB.T.DVLD.Entities.Helper.LookupService;

namespace MB.T.DVLD.Web.Controllers
{
    public class OfficerController : Controller
    {
        #region Data and Constructors 

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILookupService _lookupService;

        public OfficerController(ApplicationDbContext context,
                                 IMapper mapper,
                                 ILookupService lookupService)
        {
            _context = context;
            _mapper = mapper;
            _lookupService = lookupService;
        }

        #endregion

        #region Actions

        public async Task<IActionResult> Index()
        {
            List<Officer> officer = await _context
                                            .Officers
                                            .Include(officer => officer.Department)
                                            .Include(officer => officer.PoliceCars)
                                            .ToListAsync();

            List<OfficerVM> officerVMs = _mapper.Map<List<Officer>, List<OfficerVM>>(officer);

            ViewBag.PoliceCarSelectList = await _lookupService.GetPoliceCarSelectList();

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
                                        .Include(officer => officer.Department)
                                        .Include(officer => officer.PoliceCars)
                                        .Where(officer => officer.Id == id)
                                        .FirstOrDefaultAsync();

            if (officer == null)
            {
                return NotFound();
            }

            OfficerVM officerVM = _mapper.Map<Officer, OfficerVM>(officer);

            return View(officerVM);
        }

        public async Task<IActionResult> CreateAsync()
        {
            var officerVM = new OfficerVM();
            officerVM.DepartmentSelectList = await _lookupService.GetDepartmentSelectList();

            return View(officerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfficerVM officerVM)
        {
            if (ModelState.IsValid)
            {
                bool isBadgeNumberUsed = CheckIfBadgeIsUsed(officerVM.BadgeNumber);
                if (isBadgeNumberUsed)
                {
                    ModelState.AddModelError("BadgeNumber", "Badge number already used");
                }
                else
                {
                    Officer officer = _mapper.Map<OfficerVM, Officer>(officerVM);

                    _context.Add(officer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                
            }
            officerVM.DepartmentSelectList = await _lookupService.GetDepartmentSelectList();
            return View(officerVM);
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
            officerVM.DepartmentSelectList = await _lookupService.GetDepartmentSelectList();

            return View(officerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OfficerVM officerVM)
        {
            if (id != officerVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool isBadgeNumberUsed = CheckIfBadgeIsUsed(officerVM.BadgeNumber,officerVM.Id);
                    if (isBadgeNumberUsed)
                    {
                        ModelState.AddModelError("BadgeNumber", "Badge number already used");
                    }
                    else
                    {
                        Officer officer = _mapper.Map<OfficerVM, Officer>(officerVM);

                        _context.Update(officer);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfficerExists(officerVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            officerVM.DepartmentSelectList = await _lookupService.GetDepartmentSelectList();
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

        [HttpPost]
        public async Task<ActionResult> AssignCarToOfficer(int id, List<int> policeCarIds)
        {
            var officer = await _context.Officers.FindAsync(id);

            var policeCars = await _context
                                    .PoliceCar
                                    .Where(driver => policeCarIds.Contains(driver.Id))
                                    .ToListAsync();

            officer.PoliceCars.AddRange(policeCars);
            _context.Update(officer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool OfficerExists(int id)
        {
            return _context.Officers.Any(e => e.Id == id);
        }
        private bool CheckIfBadgeIsUsed(string badgeNumber, int officerId = 0) 
        {
            if (officerId == 0) // Zero means Create New officer
            {
                return _context.Officers.Any(c => c.BadgeNumber == badgeNumber);
            }
            else // means Edit officer
            {
                return _context.Officers.Any(c => c.BadgeNumber == badgeNumber && c.Id != officerId);
            }
        }

        #endregion
    }
}
