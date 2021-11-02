using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryApp.Model.ViewEntity
{
    public class ViewPayment
    {
        public int PaymentId { get; set; }
        public string PaymentToken { get; set; }
        public double PaymentAmount { get; set; }
        public int OrderId { get; set; }
    }
}
