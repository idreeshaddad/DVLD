using System.ComponentModel.DataAnnotations;

namespace MB.T.DVLD.Web.Models.Departments
{
    public class DepartmentVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }
    }
}
