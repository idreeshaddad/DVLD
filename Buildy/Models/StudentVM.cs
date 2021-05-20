using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Buildy.Data.Entities
{
    public class StudentVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Fullname
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public DateTime DateOfBirth { get; set; }
    }
}
