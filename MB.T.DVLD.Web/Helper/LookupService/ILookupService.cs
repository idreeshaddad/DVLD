using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace MB.T.DVLD.Entities.Helper.LookupService
{
    public interface ILookupService
    {
        public Task<SelectList> GetCarSelectList();
        public Task<SelectList> GetDriverSelectList();
        public Task<SelectList> GetOfficerSelectList();
        public Task<SelectList> GetInsuranceSelectList();
        public Task<SelectList> GetDepartmentSelectList();
    }
}
