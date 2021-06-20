using Cozmo.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cozmo.Models.Customer
{
    public class CustomerVM
    {
        public CustomerVM()
        {
            Products = new List<ProductVM>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductVM> Products { get; set; }
    }
}
