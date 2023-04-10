using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProiectWebService;
using ProiectWebService.Dtos;
using ProiectWebService.Services.Interfaces;

namespace ProiectWeb.Controllers
{
    [ApiController]
    public class ItemsController : Controller
    {
        private readonly IItemService _itemService;
        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<ItemsDto>>>> GetAllItems()
        {
            return Ok(await _itemService.GetAllItems());
        }
    }
}
