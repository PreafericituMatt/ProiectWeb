using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProiectWebService;
using ProiectWebService.Dtos;
using ProiectWebService.Services.Interfaces;

namespace ProiectWeb.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class ItemsController : Controller
    {
        private readonly IItemService _itemService;
        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<ItemsDto>>>> GetAll()
        {
            return Ok(await _itemService.GetAllItems());
        }

        [HttpGet("GetPopular")]
        public async Task<ActionResult<ServiceResponse<List<ItemsDto>>>> GetPopular()
        {
            return Ok(await _itemService.GetAllItems());
        }

        [HttpGet("GetPromoted")]
        public async Task<ActionResult<ServiceResponse<List<ItemsDto>>>> GetPromoted()
        {
            return Ok(await _itemService.GetAllItems());
        }
    }
}
