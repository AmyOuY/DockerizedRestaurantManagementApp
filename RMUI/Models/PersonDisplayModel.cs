using RMDataLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RMUI.Models
{
    public class PersonDisplayModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Employee ID")]
        [Range(100000, 999999, ErrorMessage = "You need to provide a valid ID")]
        public int EmployeeID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "You need to provide a long enough First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "You need to provide a long enough Last Name")]
        public string LastName { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }
    }
}
