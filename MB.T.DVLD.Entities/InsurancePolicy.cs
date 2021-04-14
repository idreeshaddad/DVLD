using System;
using MB.T.DVLD.Utilities.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.T.DVLD.Entities
{
    public class InsurancePolicy
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ExpiryDate { get; set; }
        public InsuranceType InsuranceType { get; set; }
        public InsuranceCompany InsuranceCompany { get; set; }
        public virtual Car Car { get; set; }
    }
}
