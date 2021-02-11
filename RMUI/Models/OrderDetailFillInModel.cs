using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RMUI.Models
{
    public class OrderDetailFillInModel
    {
        [Required]
        [Display(Name = "Dining Table Number")]
        public int DiningTableId { get; set; }

        [Required]
        [Display(Name = "Attendant Name")]
        public int AttendantId { get; set; }

        [Required]
        [Display(Name = "Food Type")]
        public int FoodTypeId { get; set; }

        [Required]
        [Display(Name = "Food Name")]
        public int FoodId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "You need to enter a valid Quantity")]
        public int Quantity { get; set; }
    }
}
