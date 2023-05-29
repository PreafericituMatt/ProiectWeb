using System.ComponentModel.DataAnnotations;

namespace ProiectWeb.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public OrderDetails OrderDetails { get; set; }
        public List<OrderItems> OrderItems { get; set;}
        public string DeliveryStatus { get; set; }
        public float TotalAmount { get; set; }
    }
}
