using MB.T.DVLD.Web.Data;
using MB.T.DVLD.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MB.T.DVLD.Entities.Helper.LookupService
{
    public class LookupService : ILookupService
    {
        private readonly ApplicationDbContext _context;

        public LookupService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SelectList> GetCarSelectList()
        {
            var carsLookup = await _context
                                    .Cars
                                    .Select(car => new LookupVM()
                                    {
                                        Id = car.Id,
                                        Name = $"{car.Manu} - {car.Model} - {car.LicensePlate}"
                                    })
                                    .ToListAsync();

            var carSelectList = new SelectList(carsLookup, "Id", "Name");

            return carSelectList;
        }

        public async Task<SelectList> GetDriverSelectList()
        {
            var driversLookup = await _context
                                    .Drivers
                                    .Select(driver => new LookupVM()
                                    {
                                        Id = driver.Id,
                                        Name = $"{driver.FirstName} {driver.LastName}"
                                    })
                                    .ToListAsync();

            var driverSelectList = new SelectList(driversLookup, "Id", "Name");

            return driverSelectList;
        }

        public async Task<SelectList> GetOfficerSelectList()
        {
            var officersLookup = await _context
                                    .Officers
                                    .Select(officer => new LookupVM()
                                    {
                                        Id = officer.Id,
                                        Name = $"{officer.FirstName} {officer.LastName} - {officer.Rank}"
                                    })
                                    .ToListAsync();

            var driverSelectList = new SelectList(officersLookup, "Id", "Name");

            return driverSelectList;
        }

        public async Task<SelectList> GetInsuranceCompanySelectList() 
        {
            var insuranceCompanyLookup = await _context
                                    .InsuranceCompanies
                                    .Select(insurance => new LookupVM()
                                    {
                                        Id = insurance.Id,
                                        Name = insurance.CompanyName
                                    })
                                    .ToListAsync();

            var insuranceCompanySelectList = new SelectList(insuranceCompanyLookup, "Id", "Name");

            return insuranceCompanySelectList;

        }

        public async Task<SelectList> GetDepartmentSelectList()
        {
            var departmentsLookup = await _context
                                            .Departments
                                            .Select(department => new LookupVM()
                                            {
                                                Id = department.Id,
                                                Name = department.Name
                                            })
                                            .ToListAsync();

            var departmentSelectList = new SelectList(departmentsLookup, "Id", "Name");

            return departmentSelectList;
        }

        public async Task<SelectList> GetPoliceCarSelectList()
        {
            var policeCarsLookup = await _context
                                            .PoliceCar
                                            .Select(policeCar => new LookupVM()
                                            {
                                                Id = policeCar.Id,
                                                Name = $"{policeCar.Manu} - {policeCar.Model} - {policeCar.CarNumber}" 
                                            })
                                            .ToListAsync();

            var policeCarSelectList = new SelectList(policeCarsLookup, "Id", "Name");

            return policeCarSelectList;
        }
    }
}
