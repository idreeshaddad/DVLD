using DVLD.Data;
using DVLD.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DVLD.Helper.LookupService
{
    public class LookupService : ILookupService
    {
        private readonly ApplicationDbContext _context;

        public LookupService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SelectList> GetCarsListItems()
        {
            var carsLookup = await _context
                                    .Cars
                                    .Select(car => new LookupVM()
                                    {
                                        Id = car.Id,
                                        Name = $"{car.Manu} - {car.Model} - {car.LicensePlate}"
                                    })
                                    .ToListAsync();

            var carsListItems = new SelectList(carsLookup, "Id", "Name");

            return carsListItems;
        }

        public async Task<SelectList> GetDriversListItems()
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

        public async Task<SelectList> GetOfficersListItems()
        {
            var officersLookup = await _context
                                    .Officers
                                    .Select(officer => new LookupVM()
                                    {
                                        Id = officer.Id,
                                        Name = $"{officer.FirstName} {officer.LastName} - {officer.Rank}"
                                    })
                                    .ToListAsync();

            var driversListItems = new SelectList(officersLookup, "Id", "Name");

            return driversListItems;
        }
    }
}
