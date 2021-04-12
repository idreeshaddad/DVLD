using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using MB.T.DVLD.Web.Data;
using MB.T.DVLD.Web.Models.Car;
using MB.T.DVLD.Web.Models.Driver;
using MB.T.DVLD.Entities;
using Microsoft.Extensions.Logging;
using System;

namespace MB.T.DVLD.Web.Controllers
{
    [Authorize]
    public class DriverController : Controller
    {
        #region Data and Constructor

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DriverController> _logger;

        public DriverController(ApplicationDbContext context, 
                                IMapper mapper,
                                ILogger<DriverController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        #endregion

        #region Actions

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {
                //int x = 10;
                //int y = 0;
                //int z = x / y;

                var drivers = await _context.Drivers.ToListAsync();
                var driverVMs = _mapper.Map<List<Driver>, List<DriverVM>>(drivers);
                return View(driverVMs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Driver list فقعت");
                return BadRequest();
            }
        }

        //[AllowAnonymous]
        //public async Task<IActionResult> DriverCars(int id)
        //{
        //    string driverName = await _context
        //                            .Drivers
        //                            .Where(driver => driver.Id == id)
        //                            .Select(driver => $"{driver.FirstName} {driver.LastName}")
        //                            .SingleAsync();


        //    var driverCars = await _context
        //                                    .Cars
        //                                    .Include(car => car.Drivers)
        //                                    .Where(car => car.Driver.Id == id)
        //                                    .ToListAsync();

        //    var carVMs = _mapper.Map<List<Car>, List<CarVM>>(driverCars);

        //    return View(carVMs);
        //}

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers
                .FirstOrDefaultAsync(m => m.Id == id);

            if (driver == null)
            {
                return Redirect("~/errorPages/DriverNotFound.html");
            }

            var driverVM = _mapper.Map<Driver, DriverVM>(driver);

            return View(driverVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DriverVM driverVM)
        {
            if (ModelState.IsValid)
            {
                var driver = _mapper.Map<DriverVM, Driver>(driverVM);

                _context.Add(driver);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(driverVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers.FindAsync(id);

            if (driver == null)
            {
                return NotFound();
            }

            var driverVM = _mapper.Map<Driver, DriverVM>(driver);

            return View(driverVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DriverVM driverVM)
        {
            if (id != driverVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var driver = _mapper.Map<DriverVM, Driver>(driverVM);

                    _context.Update(driver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverExists(driverVM.Id))
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

            return View(driverVM);
        }

       

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool DriverExists(int id)
        {
            return _context.Drivers.Any(e => e.Id == id);
        }

        #endregion

    }
}
