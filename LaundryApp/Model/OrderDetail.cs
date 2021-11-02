using System;
using System.ComponentModel.DataAnnotations;


namespace LaundryApp.Model
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public double Amount { get; set; }
        public bool Status { get; set; }

    }
}
