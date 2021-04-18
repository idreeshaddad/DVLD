using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MB.T.DVLD.Entities;
using MB.T.DVLD.Web.Data;
using MB.T.DVLD.Web.Models.InsurancePolicies;
using AutoMapper;
using MB.T.DVLD.Entities.Helper.LookupService;

namespace MB.T.DVLD.Web.Controllers
{
    public class InsurancePolicyController : Controller
    {
        #region Data and Constructor

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILookupService _lookupService;

        public InsurancePolicyController(ApplicationDbContext context,
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
            return View(await _context.InsurancePolicies.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurancePolicy = await _context.InsurancePolicies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insurancePolicy == null)
            {
                return NotFound();
            }

            return View(insurancePolicy);
        }

        public async Task<IActionResult> CreateAsync()
        {
            var vm = new CreateEditInsurancePolicyVM();

            vm.CompanySelectList = await _lookupService.GetInsuranceCompanySelectList();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsurancePolicy insurancePolicy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insurancePolicy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insurancePolicy);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurancePolicy = await _context.InsurancePolicies.FindAsync(id);
            if (insurancePolicy == null)
            {
                return NotFound();
            }
            return View(insurancePolicy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InsurancePolicy insurancePolicy)
        {
            if (id != insurancePolicy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insurancePolicy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsurancePolicyExists(insurancePolicy.Id))
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
            return View(insurancePolicy);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insurancePolicy = await _context.InsurancePolicies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insurancePolicy == null)
            {
                return NotFound();
            }

            return View(insurancePolicy);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insurancePolicy = await _context.InsurancePolicies.FindAsync(id);
            _context.InsurancePolicies.Remove(insurancePolicy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool InsurancePolicyExists(int id)
        {
            return _context.InsurancePolicies.Any(e => e.Id == id);
        } 

        #endregion
    }
}
