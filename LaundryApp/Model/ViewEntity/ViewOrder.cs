using System.ComponentModel.DataAnnotations;

namespace LaundryApp.Model.ViewEntity
{
    public class ViewOrder
    {
        public int OrderId { get; set; }
        public float TotalAmount { get; set; }
        public int OrderStatus { get; set; }
        public int AccountId { get; set; }
    }
}
