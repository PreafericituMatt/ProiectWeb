namespace ProiectWebService.Dtos
{
    public class OrderDetailsDto
    {     
        public string FirstName { get; set; }     

        public string LastName { get; set; }

        public AddressDto DeliveryAddress { get; set; }    

        public AddressDto BillingAddress { get; set; }

        public bool UseBillingAddrForDelivery { get; set; } = true;

        public bool PaymentTypeOnline { get; set; }

        [DeliveryDateValidation]
        public DateTime DeliveryDate { get; set; }

        public string? Observations { get; set; }
    }
}
