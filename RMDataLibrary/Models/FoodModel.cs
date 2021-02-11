using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataLibrary.Models
{
    public class FoodModel
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string FoodName { get; set; }
        public decimal Price { get; set; }
    }
}
