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
                                .Include(ticket => ticket.Officer)
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

            createEditTicketVM.DriverSelectList = await _lookupService.GetDriverSelectList();
            createEditTicketVM.CarSelectList = await _lookupService.GetCarSelectList();
            createEditTicketVM.OfficerSelectList = await _lookupService.GetOfficerSelectList();
            createEditTicketVM.DepartmentSelectList = await _lookupService.GetDepartmentSelectList();

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


            var createEditTicketVM = new CreateEditTicketVM();

            createEditTicketVM.DriverSelectList = await _lookupService.GetDriverSelectList();
            createEditTicketVM.CarSelectList = await _lookupService.GetCarSelectList();
            createEditTicketVM.OfficerSelectList = await _lookupService.GetOfficerSelectList();
            createEditTicketVM.DepartmentSelectList = await _lookupService.GetDepartmentSelectList();

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
            ticketVM.DriverSelectList = await _lookupService.GetDriverSelectList();
            ticketVM.CarSelectList = await _lookupService.GetCarSelectList();
            ticketVM.OfficerSelectList = await _lookupService.GetOfficerSelectList();
            ticketVM.DepartmentSelectList = await _lookupService.GetDepartmentSelectList();


            return View(ticketVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateEditTicketVM ticketVM)
        {
            if (id != ticketVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var ticket = _mapper.Map<CreateEditTicketVM, Ticket>(ticketVM);



                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticketVM.Id))
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
            return View(ticketVM);
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PayTicket(int ticketId)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);

            ticket.Paid = true;
            ticket.PaymentDate = DateTime.Now;

            _context.Update(ticket);
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
