using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace MB.T.DVLD.Entities.Helper.LookupService
{
    public interface ILookupService
    {
        public Task<SelectList> GetCarsListItems();
        public Task<SelectList> GetDriversListItems();
        public Task<SelectList> GetOfficersListItems();
        public Task<SelectList> GetInsurancesListItems();
    }
}
