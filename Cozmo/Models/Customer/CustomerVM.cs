using Cozmo.Models.Product;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

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

        public SelectList ProductsSelectList { get; set; }
    }
}
