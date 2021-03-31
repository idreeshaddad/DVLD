using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace DVLD.Helper.LookupService
{
    public interface ILookupService
    {
        public Task<SelectList> GetCarsListItems();
        public Task<SelectList> GetDriversListItems();
        public Task<SelectList> GetOfficersListItems();
    }
}
