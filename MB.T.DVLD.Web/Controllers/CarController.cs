using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using AutoMapper;
using MB.T.DVLD.Web.Data;
using MB.T.DVLD.Web.Models.Car;
using MB.T.DVLD.Entities.Helper.LookupService;
using MB.T.DVLD.Entities;

namespace MB.T.DVLD.Web.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        #region Data and Constructors

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILookupService _lookupService;

        public CarController(ApplicationDbContext context,
                            IMapper mapper,
                            ILookupService lookupService)
        {
            _context = context;
            _mapper = mapper;
            _lookupService = lookupService;
        }

        #endregion

        #region Actions

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            List<Car> cars = await _context
                                        .Cars
                                        .Include(car => car.Driver)
                                        .Include(car => car.Insurance)
                                        .ToListAsync();

            List<CarVM> carsVMs = _mapper.Map<List<Car>, List<CarVM>>(cars);

            return View(carsVMs);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context
                                .Cars
                                .Include(car => car.Driver)
                                .Include(car => car.Insurance)
                                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                return Redirect("~/errorPages/CarNotFound.html");
            }

            CarVM carVM = _mapper.Map<Car, CarVM>(car);

            return View(carVM);
        }

        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.DriversListItems = await _lookupService.GetDriversListItems();
            ViewBag.GetInsurancesListItems = await _lookupService.GetInsurancesListItems();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditCarVM createEditCarVM)
        {
            if (ModelState.IsValid)
            {
                var car = _mapper.Map<CreateEditCarVM, Car>(createEditCarVM);

                if (createEditCarVM.DriverId > 0)
                {
                    var driver = await _context.Drivers.FindAsync(createEditCarVM.DriverId);
                    car.Driver = driver;
                }

                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(createEditCarVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context
                                .Cars
                                .Include(car => car.Driver)
                                .Include(car => car.Insurance)
                                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            ViewBag.DriversListItems = await _lookupService.GetDriversListItems();
            ViewBag.InsurancesListItems = await _lookupService.GetInsurancesListItems();

            var carVM = _mapper.Map<Car, CreateEditCarVM>(car);

            return View(carVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateEditCarVM createEditCarVM)
        {
            if (id != createEditCarVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var car = _mapper.Map<CreateEditCarVM, Car>(createEditCarVM);

                    if (createEditCarVM.DriverId == 0)
                    {
                        car.DriverId = null;
                    }
                    else
                    {
                        var driver = await _context.Drivers.FindAsync(createEditCarVM.DriverId);
                        car.Driver = driver;
                        var Insurance = await _context.Insurances.FindAsync(createEditCarVM.InsuranceId);
                        car.Insurance = Insurance;
                    }

                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(createEditCarVM.Id))
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
            return View(createEditCarVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }

        #endregion
    }
}
