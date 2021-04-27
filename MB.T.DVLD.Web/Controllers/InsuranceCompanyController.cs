using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MB.T.DVLD.Entities;
using MB.T.DVLD.Web.Data;
using AutoMapper;
using MB.T.DVLD.Web.Models.InsuranceCompanies;
using System;
using Microsoft.Extensions.Logging;
using MB.T.DVLD.Web.Models;
using MB.T.DVLD.Web.Models.Errors;
using MB.T.DVLD.Web.Models.Car;

namespace MB.T.DVLD.Web.Controllers
{
    public class InsuranceCompanyController : Controller
    {
        #region Data And Constructor

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<InsuranceCompanyController> _logger;

        public InsuranceCompanyController(ApplicationDbContext context,
                                          IMapper mapper,
                                          ILogger<InsuranceCompanyController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> Index()
        {
            var insuranceCompaines = await _context.InsuranceCompanies.ToListAsync();

            var insuranceCompanyVM = _mapper.Map<List<InsuranceCompany>, List<InsuranceCompanyVM>>(insuranceCompaines);

            return View(insuranceCompanyVM);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceCompany = await _context.InsuranceCompanies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuranceCompany == null)
            {
                return NotFound();
            }

            var insuranceCompanyVM = _mapper.Map<InsuranceCompany, InsuranceCompanyVM>(insuranceCompany);

            return View(insuranceCompanyVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsuranceCompanyVM insuranceCompanyVM)
        {
            if (ModelState.IsValid)
            {
                var insuranceCompany = _mapper.Map<InsuranceCompanyVM, InsuranceCompany>(insuranceCompanyVM);

                _context.Add(insuranceCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceCompanyVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceCompany = await _context.InsuranceCompanies.FindAsync(id);

            if (insuranceCompany == null)
            {
                return NotFound();
            }

            var insuranceCompanyVM = _mapper.Map<InsuranceCompany, InsuranceCompanyVM>(insuranceCompany);

            return View(insuranceCompanyVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InsuranceCompany insuranceCompany)
        {
            if (id != insuranceCompany.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuranceCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceCompanyExists(insuranceCompany.Id))
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
            return View(insuranceCompany);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insuranceCompany = await _context.InsuranceCompanies.FindAsync(id);

            try
            {
                _context.InsuranceCompanies.Remove(insuranceCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"{User.Identity.Name} is trying to delete company {insuranceCompany.CompanyName}, with id: {id}");

                var cars = await _context
                                    .Cars
                                    .Include(car => car.InsurancePolicy)
                                    .Where(car => car.InsurancePolicy.InsuranceCompanyId == id)
                                    .ToListAsync();

                var error = new InsuranceCompanyErrorVM()
                {
                    Message = $"You cannot delete insurance company: ({insuranceCompany.CompanyName}) because those cars have policies issued by it:",
                    Cars = _mapper.Map<List<Car>, List<CarVM>>(cars),
                    DateNow = DateTime.Now
                };

                return View("InsuranceCompanyError", error);
            }
        }

        #endregion

        #region Private Actions
        private bool InsuranceCompanyExists(int id)
        {
            return _context.InsuranceCompanies.Any(e => e.Id == id);
        }
        #endregion
    }
}
