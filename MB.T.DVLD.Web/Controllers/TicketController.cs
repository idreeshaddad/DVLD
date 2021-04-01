using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using System;
using MB.T.DVLD.Web.Data;
using MB.T.DVLD.Web.Models.Tickets;
using MB.T.DVLD.Entities.Helper.LookupService;
using MB.T.DVLD.Entities;

namespace MB.T.DVLD.Web.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        #region Data and Constructors

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILookupService _lookupService;

        public TicketController(ApplicationDbContext context,
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
            var tickets = await _context
                                            .Tickets
                                            .Include(ticket => ticket.Driver)
                                            .Include(ticket => ticket.Car)
                                            .Include(ticket => ticket.Officer)
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
            var createEditTicketVM = new CreateEditTicketVM();

            createEditTicketVM.DriversListItems = await _lookupService.GetDriversListItems();
            createEditTicketVM.CarsListItems = await _lookupService.GetCarsListItems();
            createEditTicketVM.OfficersListItems = await _lookupService.GetOfficersListItems();

            createEditTicketVM.IssueDate = DateTime.Now;

            return View(createEditTicketVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditTicketVM ticketVM)
        {
            if (ModelState.IsValid)
            {
                var ticket = _mapper.Map<CreateEditTicketVM, Ticket>(ticketVM);

                var driver = await _context.Drivers.FindAsync(ticketVM.DriverId);
                ticket.Driver = driver;

                var car = await _context.Cars.FindAsync(ticketVM.CarId);
                ticket.Car = car;

                var officer = await _context.Officers.FindAsync(ticketVM.OfficerId);
                ticket.Officer = officer;

                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            ViewBag.DriversListItems = await _lookupService.GetDriversListItems();
            ViewBag.CarsListItems = await _lookupService.GetCarsListItems();
            ViewBag.OfficersListItems = await _lookupService.GetOfficersListItems();
            return View(ticketVM);
        }

        public async Task<IActionResult> Edit(int? id)
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

            var ticketVM = _mapper.Map<Ticket, CreateEditTicketVM>(ticket);
            ticketVM.DriversListItems = await _lookupService.GetDriversListItems();
            ticketVM.CarsListItems = await _lookupService.GetCarsListItems();
            ticketVM.OfficersListItems = await _lookupService.GetOfficersListItems();

            return View(ticketVM);
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

        #endregion
    }
}
