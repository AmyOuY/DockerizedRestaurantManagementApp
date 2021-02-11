using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataLibrary.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int DiningTableId { get; set; }
        public int AttendantId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool BillPaid { get; set; }
    }
}
