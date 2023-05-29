using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectWeb.Entities
{
    public class ShoppingCartItems
    {
        [ForeignKey("ShoppingCartId")]
        public int ShoppingCartId { get; set; }
        [Key]
        public int ShoppingCartItemsId { get; set; }
        [ForeignKey("ItemId")]
        public int ItemId { get; set; } 
        public int Quantity { get; set; }   
        public float FinalAmount { get; set; }
    }
}
