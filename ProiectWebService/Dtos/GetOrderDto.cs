namespace ProiectWebService.Dtos
{
    public class GetOrderDto
    {
        public int OrderId { get; set; }
        public OrderDetailsDto OrderDetails { get; set; }
        public List<OrderItemsDto> OrderItems { get; set; }
    }
}
