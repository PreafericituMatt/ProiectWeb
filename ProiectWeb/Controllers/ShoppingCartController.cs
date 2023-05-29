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
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;    

        public ShoppingCartController(IShoppingCartService shoppingCartService, IHttpContextAccessor httpContextAccessor)
        {
            _shoppingCartService = shoppingCartService;          
        }

        [HttpGet("GetAllShoppingCarts")]

        public async Task<ActionResult<ServiceResponse<List<ShoppingCartDto>>>> GetAllItems()
        {           
            return Ok(await _shoppingCartService.GetAllItems());
        }

        [HttpPost("AddToCart")]
        public async Task<ActionResult<ServiceResponse<ShoppingCartDto>>> AddToCart(int bookId,int quantity)
        {
            var identity = GetEmail();
            var userId = Convert.ToInt32(identity);

            return Ok(await _shoppingCartService.AddItems(bookId,quantity,userId));
        }

        [HttpGet("GetCurrentCart")]
        public async Task<ActionResult<ServiceResponse<List<ShoppingCartItemsDto>>>> GetCurrent()
        {
            var identity = GetEmail();
            var userId = Convert.ToInt32(identity);

            return Ok(await _shoppingCartService.GetCurrentShoppingCart(userId));
        }

        [HttpDelete("DeleteCart")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteCart(int shoppingCartId)
        {
            var identity = GetEmail();
            var userId = Convert.ToInt32(identity);

            return Ok(await _shoppingCartService.DeleteShoppingCartItem( shoppingCartId, userId));
        }

        private string GetEmail()
        {
            var claims = (ClaimsIdentity)User.Identity;
            return claims.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
