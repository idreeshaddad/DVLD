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
using MB.T.DVLD.Web.Models.Cars;
using System;

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
                                        .Include(car => car.Drivers)
                                        .Include(car => car.InsurancePolicy)
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
                                .Include(car => car.Drivers)
                                .Include(car => car.InsurancePolicy)
                                .Include(car => car.InsurancePolicy.InsuranceCompany)
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
            var createEditCarVM = new CreateEditCarVM()
            {
                DriverSelectList = await _lookupService.GetDriverSelectList()
            };

            return View(createEditCarVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditCarVM createEditCarVM)
        {
            if (ModelState.IsValid)
            {
                bool isLicensePlateUsed = CheckIfLicensePlateIsUsed(createEditCarVM.LicensePlate);

                if (isLicensePlateUsed)
                {
                    ModelState.AddModelError("LicensePlate", "License Plate already used");
                }
                else
                {
                    var car = _mapper.Map<CreateEditCarVM, Car>(createEditCarVM);

                    await AddDriversToCar(createEditCarVM, car);

                    _context.Add(car);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }


            createEditCarVM.DriverSelectList = await _lookupService.GetDriverSelectList();
            //createEditCarVM.InsuranceSelectList = await _lookupService.GetInsuranceCompanySelectList();

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
                                .Include(car => car.Drivers)
                                .Include(car => car.InsurancePolicy)
                                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            var createEditCarVM = _mapper.Map<Car, CreateEditCarVM>(car);

            createEditCarVM.DriverSelectList = await _lookupService.GetDriverSelectList();

            return View(createEditCarVM);
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
                    bool isLicensePlateUsed = CheckIfLicensePlateIsUsed(createEditCarVM.LicensePlate, createEditCarVM.Id);

                    if (isLicensePlateUsed)
                    {
                        ModelState.AddModelError("LicensePlate", "License Plate already used");
                    }
                    else
                    {
                        var car = _mapper.Map<CreateEditCarVM, Car>(createEditCarVM);

                        _context.Update(car);
                        await _context.SaveChangesAsync();

                        await UpdateCarDrivers(createEditCarVM.Id, createEditCarVM.DriverIds);

                        return RedirectToAction(nameof(Index));
                    }
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


        public async Task<IActionResult> IssuePolicy(int carId)
        {
            var carPolicyVM = new CarPolicyVM();

            carPolicyVM.CompanySelectList = await _lookupService.GetInsuranceCompanySelectList();
            carPolicyVM.CarId = carId;
            carPolicyVM.StartTime = DateTime.Now;
            carPolicyVM.ExpiryDate = DateTime.Now.AddYears(1);

            return View(carPolicyVM);
        }


        [HttpPost]
        public async Task<IActionResult> IssuePolicy(CarPolicyVM carPolicyVM)
        {
            if (ModelState.IsValid)
            {
                var insurancePolicy = _mapper.Map<CarPolicyVM, InsurancePolicy>(carPolicyVM);
                _context.Add(insurancePolicy);
                await _context.SaveChangesAsync();

                var car = await _context.Cars.FindAsync(carPolicyVM.CarId);
                car.InsurancePolicyId = insurancePolicy.Id;
                _context.Update(car);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }


            carPolicyVM.CompanySelectList = await _lookupService.GetInsuranceCompanySelectList();
            return View(carPolicyVM);
        }

        #endregion

        #region Private Methods

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }

        private bool CheckIfLicensePlateIsUsed(string licensePlate, int carId = 0)
        {
            if (carId == 0) // Zero means Create New Car
            {
                return _context.Cars.Any(c => c.LicensePlate == licensePlate);
            }
            else // means Edit car
            {
                return _context.Cars.Any(c => c.LicensePlate == licensePlate && c.Id != carId);
            }
        }

        private async Task AddDriversToCar(CreateEditCarVM createEditCarVM, Car car)
        {
            if (createEditCarVM.DriverIds != null && createEditCarVM.DriverIds.Count > 0)
            {
                var drivers = await _context
                                        .Drivers
                                        .Where(driver => createEditCarVM.DriverIds.Contains(driver.Id))
                                        .ToListAsync();

                car.Drivers.AddRange(drivers);
            }
        }

        private async Task UpdateCarDrivers(int carId, List<int> driverIds)
        {
            if (driverIds != null && driverIds.Count > 0)
            {
                // Load the car and drivers
                Car car = await _context
                                        .Cars
                                        .Include(car => car.Drivers)
                                        .Where(car => car.Id == carId)
                                        .SingleAsync();
                // Delete car drivers 
                car.Drivers.Clear();

                // Load drivers  with IDs sent from the UI
                var drivers = await _context
                                        .Drivers
                                        .Where(driver => driverIds.Contains(driver.Id))
                                        .ToListAsync();
                
                // Add drivers to car
                car.Drivers.AddRange(drivers);

                // SAVE
                _context.Update(car);
                await _context.SaveChangesAsync();
            }
            else
            {
                // ELSE means: NO drivers are assigned to the car

                // Load the car and drivers
                Car car = await _context
                                        .Cars
                                        .Include(car => car.Drivers)
                                        .Where(car => car.Id == carId)
                                        .SingleAsync();

                // Delete car drivers 
                car.Drivers.Clear();

                // SAVE
                _context.Update(car);
                await _context.SaveChangesAsync();
            }
        }

        #endregion
    }
}
