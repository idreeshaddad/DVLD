using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cozmo.Data;
using Entites;
using AutoMapper;
using Cozmo.Models.Customer;
using Cozmo.Helper.LookUpService;

namespace Cozmo.Controllers
{
    public class CustomersController : Controller
    {
        #region Data and Const
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILookUpService _lookUpService;

        public CustomersController(ApplicationDbContext context,IMapper mapper, ILookUpService lookUpService)
        {
            _context = context;
            _mapper = mapper;
            _lookUpService = lookUpService;
        }
        #endregion

        #region Public Actions
        public async Task<IActionResult> Index()
        {
            var customer = await _context
                                         .Customers
                                         .Include(x => x.Products)
                                         .ToListAsync();

            var customerVM = _mapper.Map<List<Customer>, List<CustomerVM>>(customer);

            return View(customerVM);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context
                                         .Customers
                                         .Include(x => x.Products)
                                         .FirstOrDefaultAsync(x => x.Id == id);
                                        

            if (customer == null)
            {
                return NotFound();
            }

            var customerVM = _mapper.Map<Customer, CustomerVM>(customer);



            return View(customerVM);
        }
        public async Task<IActionResult> CustomerProducts(int? id)
        {
            var customer = await _context
                                         .Customers
                                         .Include(x => x.Products)
                                         .Where(x => x.Id == id)
                                         .FirstOrDefaultAsync();  

            var customerVM = _mapper.Map<Customer,CustomerVM>(customer);

            return View(customerVM);
        }
        public IActionResult Create()
        {                      
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerVM customerVM)
        {
            if (ModelState.IsValid)
            {
                var customer = _mapper.Map<CustomerVM, Customer> (customerVM);

                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(customerVM);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            var customerVM = _mapper.Map<Customer, CustomerVM>(customer);

            return View(customerVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,CustomerVM customerVM)
        {
            if (id != customerVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var customer = _mapper.Map<CustomerVM, Customer>(customerVM);

                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customerVM.Id))
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
            return View(customerVM);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Private Actions
        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        } 
        #endregion
    }
}
