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
using DVLD.Models.Driver;

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

            Car car = await _context.Cars
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
            List<Driver> drivers = await _context
                                        .Drivers
                                        .ToListAsync();

            List<DriverVM> driverVMs = _mapper.Map<List<Driver>, List<DriverVM>>(drivers);

            SelectList driversListItems = new SelectList(driverVMs, "Id", "FullName");

            ViewBag.DriversListItems = driversListItems;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCarVM createCarVM)
        {
            if (ModelState.IsValid)
            {
                Car car = _mapper.Map<CreateCarVM, Car>(createCarVM);

                Driver driver = await _context.Drivers.FindAsync(createCarVM.DriverId);

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

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            return View(car);
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
