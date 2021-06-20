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
using Cozmo.Helper.LookUpService;
using Cozmo.Models.Product;

namespace Cozmo.Controllers
{
    public class ProductsController : Controller
    {
        #region Data and Const
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILookUpService _lookUpService;

        public ProductsController(ApplicationDbContext context,
                                                      IMapper mapper,
                                                      ILookUpService lookUpService)
        {
            _context = context;
            _mapper = mapper;
            _lookUpService = lookUpService;
        }
        #endregion

        #region Public Actions
        public async Task<IActionResult> Index()
        {
            var product = await _context
                                        .Products
                                        .Include(x => x.Customer)
                                        .ToListAsync();

            var productVM = _mapper.Map<List<Product>, List<ProductVM>>(product);

            return View(productVM);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context
                                        .Products
                                        .Include(x => x.Customer)
                                        .Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            var productVM = _mapper.Map<Product, ProductVM>(product);

            return View(productVM);
        }
        public async Task<IActionResult> Create()
        {
            var productCreateEditVM = new ProductCreateEditVM()
            {
                GetCustomersList = await _lookUpService.GetCustomerList()
            };

            return View(productCreateEditVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateEditVM productVM)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<ProductCreateEditVM, Product>(productVM);

                var x = await _context.Customers.FindAsync(productVM.CustomerVMId);
                product.Customer = x ;

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            productVM.GetCustomersList = await _lookUpService.GetCustomerList();

            return View(productVM);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context
                                        .Products
                                        .Include(x => x.Customer)
                                        .Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            var productVM = _mapper.Map<Product,ProductCreateEditVM>(product);

            productVM.GetCustomersList = await _lookUpService.GetCustomerList();

            return View(productVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductCreateEditVM productVM)
        {
            if (id != productVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product = _mapper.Map<ProductCreateEditVM,Product>(productVM);

                    var x = await _context.Customers.FindAsync(productVM.CustomerVMId);
                    product.Customer = x;

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(productVM.Id))
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

            productVM.GetCustomersList = await _lookUpService.GetCustomerList();

            return View(productVM);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Private Actions
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        } 
        #endregion
    }
}
