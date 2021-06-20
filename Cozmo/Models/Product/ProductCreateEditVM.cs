using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cozmo.Models.Product
{
    public class ProductCreateEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CustomerVMId { get; set; }
        public SelectList GetCustomersList { get; set; }
    }
}
