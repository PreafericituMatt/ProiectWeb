using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProiectWebData.Entities;

namespace ProiectWeb.Entities
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailsId { get; set; }

        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
     
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [InverseProperty("AddressId")]
        public int DeliveryAddressId { get; set; }
        public Address DeliveryAddress { get; set; }
        [InverseProperty("AddressId")]
        public int BillingAddressId { get; set; }
        public Address BillingAddress { get; set; }
        public bool PaymentTypeOnline { get; set; }     
        public DateTime DeliveryDate { get; set; }
        public string? Observations { get; set; }
    }
}
