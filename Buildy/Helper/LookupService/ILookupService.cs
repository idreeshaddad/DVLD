using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Buildy.Helper.LookupService
{
    public interface ILookupService
    {
        public Task<SelectList> GetNotEnrolledInCourseSelectList(int studentId);
    }
}
