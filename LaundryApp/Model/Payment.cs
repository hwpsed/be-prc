using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryApp.Model
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public string PaymentToken { get; set; }
        public double PaymentAmount { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public bool Status { get; set; }
        public int OrderId { get; set; }
    }
}
