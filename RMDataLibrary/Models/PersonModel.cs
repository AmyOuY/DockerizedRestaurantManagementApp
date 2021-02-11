using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataLibrary.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
