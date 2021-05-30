﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Buildy.Helper.LookupService
{
    public interface ILookupService
    {
        public Task<SelectList> GetCourseSelectList(int studentId);
    }
}
