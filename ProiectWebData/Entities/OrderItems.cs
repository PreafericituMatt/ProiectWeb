using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectWeb.Entities
{
    public class OrderItems
    {
        [Key]
        public int OrderItemId { get; set; }
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public float FinalAmount { get; set; }
    }
}
