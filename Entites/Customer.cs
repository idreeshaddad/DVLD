using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites
{
    public class Customer
    {
        public Customer()
        {
            Products = new List<Product>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductsId { get; set; }
        public List<Product> Products { get; set; }
    }
}
