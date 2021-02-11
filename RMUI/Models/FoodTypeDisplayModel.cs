﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RMUI.Models
{
    public class FoodTypeDisplayModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Food Type")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "You need to provide a long enough Food Type")]
        public string FoodType { get; set; }
    }
}
