using MB.T.DVLD.Web.Models.Car;
using System;
using System.Collections.Generic;

namespace MB.T.DVLD.Web.Models.Errors
{
    public class InsuranceCompanyErrorVM
    {
        public InsuranceCompanyErrorVM()
        {
            Cars = new List<CarVM>();
        }
        public string Message { get; set; }
        public List<CarVM> Cars { get; set; }
        public DateTime DateNow { get; set; }
    }
}
