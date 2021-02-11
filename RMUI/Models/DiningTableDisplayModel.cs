using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RMUI.Models
{
    public class DiningTableDisplayModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Table Number")]
        [Range(1, 100, ErrorMessage = "You need to provide a valid Table Number")]
        public int TableNumber { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "You need to provide a valid Seats number")]
        public int Seats { get; set; }
    }
}
