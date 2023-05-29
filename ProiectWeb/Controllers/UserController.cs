
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProiectWebService;
using ProiectWebService.Dtos;
using ProiectWebService.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ProiectWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<UserDto>>> Register(UserDto user)
        {
            return Ok(await _userService.Register(user));
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<AuthResponseDto>>> Login(AuthDto user)
        {
            return Ok(await _userService.Login(user));
        }

        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task<ActionResult<ServiceResponse<UserDto>>> ForgotPassword([EmailAddress] string email)
        {
            return Ok(await _userService.ForgotPassword(email));
        }     
    }
}