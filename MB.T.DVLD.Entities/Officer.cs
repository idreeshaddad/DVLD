using MB.T.DVLD.Utilities.Enums;
using System.Collections.Generic;

namespace MB.T.DVLD.Entities
{
    public class Officer
    {
        public Officer()
        {
            PoliceCars = new List<PoliceCar>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BadgeNumber  { get; set; }
        public Rank Rank { get; set; }

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        public List<PoliceCar> PoliceCars { get; set; }
    }
}
