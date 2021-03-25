using DVLD.Enums;
using System.ComponentModel.DataAnnotations;

namespace DVLD.Models.Officer
{
    public class OfficerVM
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [Required]
        public string BadgeNumber { get; set; }
        
        public Rank Rank { get; set; }
    }
}
