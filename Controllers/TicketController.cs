using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DVLD.Data;
using DVLD.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using DVLD.Models.Tickets;
using DVLD.Models;

namespace DVLD.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        #region Data and Constructors

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TicketController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        public async Task<IActionResult> Index()
        {
            var tickets = await _context
                                            .Tickets
                                            .Include(ticket => ticket.Driver)
                                            .Include(ticket => ticket.Car)
                                            .ToListAsync();

            var ticketVMs = _mapper.Map<List<Ticket>, List<TicketVM>>(tickets);

            return View(ticketVMs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context
                                .Tickets
                                .Include(ticket => ticket.Driver)
                                .Include(ticket => ticket.Car)
                                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            var ticketVM = _mapper.Map<Ticket, TicketVM>(ticket);

            return View(ticketVM);
        }

        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.DriversListItems = await GetDriversListItems();
            ViewBag.CarsListItems = await GetCarsListItems();
            ViewBag.OfficersListItems = await GetOfficersListItems();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketVM ticketVM)
        {
            if (ModelState.IsValid)
            {

                var ticket = _mapper.Map<TicketVM, Ticket>(ticketVM);

                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(ticketVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.DriversListItems = await GetDriversListItems();
            ViewBag.CarsListItems = await GetCarsListItems();
            ViewBag.OfficersListItems = await GetOfficersListItems();

            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context
                                    .Tickets
                                    .Include(ticket => ticket.Driver)
                                    .Include(ticket => ticket.Car)
                                    .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
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
            return View(ticket);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Functions

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }

        private async Task<SelectList> GetCarsListItems()
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

        private async Task<SelectList> GetOfficersListItems()
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

        #endregion
    }
}
