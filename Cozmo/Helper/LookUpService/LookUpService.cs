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
using Cozmo.Models;

namespace Cozmo.Helper.LookUpService
{
    public class LookUpService : ILookUpService
    {
        private readonly ApplicationDbContext _context;

        public LookUpService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<SelectList> GetCustomerList()
        {
            var getcustomer = await _context
                                           .Customers
                                           .Select(x => new LookUpVM()
                                           {
                                               Id = x.Id,
                                               Name = x.Name
                                           })
                                           .ToListAsync();

            var getcustomerVM = new SelectList(getcustomer, "Id","Name");

            return getcustomerVM;
        }
    }
}
