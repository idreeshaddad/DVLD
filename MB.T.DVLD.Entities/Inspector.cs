using System.Collections.Generic;

namespace MB.T.DVLD.Entities
{
    public class Inspector
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BadgeNumber { get; set; }
        public List<Department> Departments  { get; set; }
    }
}
