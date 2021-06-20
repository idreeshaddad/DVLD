using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cozmo.Helper.LookUpService
{
    public interface ILookUpService
    {
        public Task<SelectList> GetCustomerList();
    }
}
