using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MB.T.DVLD.Entities;
using MB.T.DVLD.Web.Models.Insurances;
using MB.T.DVLD.Web.Data;
using AutoMapper;


namespace MB.T.DVLD.Web.Controllers
{
    public class InsuranceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public InsuranceController(ApplicationDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        public async Task<IActionResult> Index()
        {
            var Insurance = await _context.Insurance.ToListAsync();
            var InsuranceVM = _mapper.Map<List<Insurance>, List<InsuranceVM>>(Insurance);
            return View(InsuranceVM);
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insurance == null)
            {
                return NotFound();
            }
            var InsuranceVM = _mapper.Map<Insurance, InsuranceVM>(insurance);

            return View(InsuranceVM);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsuranceVM insuranceVM)
        {
            if (ModelState.IsValid)
            {
                var Insurance = _mapper.Map<InsuranceVM, Insurance>(insuranceVM);

                _context.Add(Insurance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceVM);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance.FindAsync(id);
            if (insurance == null)
            {
                return NotFound();
            }

            var InsuranceVM = _mapper.Map<Insurance, InsuranceVM>(insurance);

            return View(InsuranceVM);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InsuranceVM insuranceVM)
        {
            if (id != insuranceVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var insurance = _mapper.Map<InsuranceVM, Insurance>(insuranceVM);
                    _context.Update(insurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceExists(insuranceVM.Id))
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
            return View(insuranceVM);
        }

        
        

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insurance = await _context.Insurance.FindAsync(id);
            _context.Insurance.Remove(insurance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceExists(int id)
        {
            return _context.Insurance.Any(e => e.Id == id);
        }
    }
}
