using System.ComponentModel.DataAnnotations;

namespace ProiectWeb.Entities
{
    public class ShoppingCart
    {
        [Key]
       public int ShoppingCartId { get; set; }
       public int UserId { get; set; }
    }
}
