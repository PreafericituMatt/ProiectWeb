using System.ComponentModel.DataAnnotations;


namespace ProiectWebData.Entities
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public string Country { get; set; }
        public string CustomerAddress { get; set; }
        public string PhoneNumber { get; set; }        
    }
}
