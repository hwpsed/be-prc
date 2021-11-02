using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LaundryApp.Model
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public float TotalAmount { get; set; }
        [Required]
        public int OrderStatus { get; set; }
        [Required]
        public int AccountId { get; set; }
        public bool isDeleted { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public virtual ICollection<OrderDetail>　OrderDetails　{ get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}