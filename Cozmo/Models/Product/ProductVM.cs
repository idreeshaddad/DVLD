using System.ComponentModel.DataAnnotations;

namespace Cozmo.Models.Product
{
    public class ProductVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
