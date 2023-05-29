
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProiectWebService;
using ProiectWebService.Dtos;
using ProiectWebService.Services.Interfaces;
using System.Security.Claims;

namespace ProiectWeb.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetUserOrders")]
        public async Task<ActionResult<ServiceResponse<List<GetOrderDto>>>> GetUserOrders()
        {
            var identity = GetEmail();
            var userId = Convert.ToInt32(identity);

            return Ok(await _orderService.GetUserOrders(userId));
        }

        [HttpPost("PlaceOrder")]
        public async Task<ActionResult<ServiceResponse<OrderDto>>> PlaceOrder(OrderDto order)
        {
            var identity = GetEmail();
            var userId = Convert.ToInt32(identity);

            return Ok(await _orderService.PlaceOrder(order, userId));
        }

        [HttpPut($"UpdateOrder/{{orderId}}")]
        public async Task<ActionResult<ServiceResponse<OrderDto>>> UpdateOrder(OrderDto updatedOrder, int orderId)
        {
            var identity = GetEmail();
            var userId = Convert.ToInt32(identity);

            return Ok(await _orderService.UpdateOrder(updatedOrder, userId, orderId));
        }

        private string GetEmail()
        {
            var claims = (ClaimsIdentity)User.Identity;
            return claims.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
