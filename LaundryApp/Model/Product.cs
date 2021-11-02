using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LaundryApp.Model
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public bool Status { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
