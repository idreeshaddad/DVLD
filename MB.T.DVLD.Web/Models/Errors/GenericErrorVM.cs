using System.Collections.Generic;

namespace MB.T.DVLD.Web.Models
{
    public class GenericErrorVM
    {
        public string Message { get; set; }
        public List<object> Items { get; set; }
    }
}
