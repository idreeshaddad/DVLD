using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DVLD.Data;
using DVLD.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using DVLD.Models.Car;
using AutoMapper;
using DVLD.Models;

namespace DVLD.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        #region Data and Constructors

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CarController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            List<Car> cars = await _context
                                        .Cars
                                        .Include(car => car.Driver)
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
                                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            CarVM carVM = _mapper.Map<Car, CarVM>(car);

            return View(carVM);
        }

        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.DriversListItems = await GetDriversListItems();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditCarVM createCarVM)
        {
            if (ModelState.IsValid)
            {
                var car = _mapper.Map<CreateEditCarVM, Car>(createCarVM);

                var driver = await _context.Drivers.FindAsync(createCarVM.DriverId);

                car.Driver = driver;

                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(createCarVM);
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
                                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            ViewBag.DriversListItems = await GetDriversListItems();

            var carVM = _mapper.Map<Car, CreateEditCarVM>(car);

            return View(carVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateEditCarVM carVM)
        {
            if (id != carVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var car = _mapper.Map<CreateEditCarVM, Car>(carVM);
                    var driver = await _context.Drivers.FindAsync(carVM.DriverId);
                    car.Driver = driver;

                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(carVM.Id))
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
            return View(carVM);
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

        private async Task<SelectList> GetDriversListItems()
        {
            var driversLookup = await _context
                                    .Drivers
                                    .Select(driver => new LookupVM()
                                    {
                                        Id = driver.Id,
                                        Name = $"{driver.FirstName} {driver.LastName}"
                                    })
                                    .ToListAsync();

            var driversListItems = new SelectList(driversLookup, "Id", "Name");

            return driversListItems;
        }

        #endregion
    }
}
